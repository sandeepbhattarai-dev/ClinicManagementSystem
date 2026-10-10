using ClinicMS.Infrastructure;
using ClinicMS.Infrastructure.Data;
using ClinicMS.Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new NullReferenceException();

builder.Services.AddInfrastructure(ConnectionString);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var dbcontext = services.GetRequiredService<ApplicationDbContext>();

  await dbcontext.Database.MigrateAsync();

  await SeedingAdminandRoles.SeedRolesAndAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
