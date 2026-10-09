using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "AppointmentSeq");

            migrationBuilder.CreateSequence<int>(
                name: "InvoiceSeq");

            migrationBuilder.CreateSequence<int>(
                name: "PatientSeq");

            migrationBuilder.CreateTable(
                name: "utblApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "utblDepartment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblDepartment", x => x.Id);
                    table.CheckConstraint("CK_utblDepartment_Name_LongerThan3Char", "LEN(TRIM([Name])) >= 3");
                });

            migrationBuilder.CreateTable(
                name: "utblPatients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "('PAT-' + RIGHT('000000' + CAST(NEXT VALUE FOR PatientSeq AS VARCHAR(6)), 6))"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BloodGroup = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblPatients", x => x.Id);
                    table.CheckConstraint("Patient_DateOfBirth_NoFuture", "[DateOfBirth] <= GetDate()");
                });

            migrationBuilder.CreateTable(
                name: "utblDoctors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ConsultationFee = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblDoctors", x => x.Id);
                    table.CheckConstraint("CK_UtblDoctor_ConsultationFee_GreaterThanZero", "[ConsultationFee] >= 0");
                    table.CheckConstraint("CK_UtblDoctor_LicenseNumber_ShouldNotBeNull", "LEN(TRIM([LicenseNumber])) > 0");
                    table.ForeignKey(
                        name: "FK_utblDoctors_utblApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "utblApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_utblDoctors_utblDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "utblDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblAppointments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('APT-' + RIGHT('000000' + CAST(NEXT VALUE FOR AppointmentSeq AS VARCHAR(6)), 6))"),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChiefComplaint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblAppointments", x => x.Id);
                    table.CheckConstraint("CK_utblAppointment_AppointmentDate_AppointmentDateInFuture", "[AppointmentDate] >= CAST(GETUTCDATE() AS DATE)");
                    table.CheckConstraint("CK_utblAppointment_EndTime_EndTimeShouldBeLaterThanStartTime", "[EndTime] > [StartTime]");
                    table.ForeignKey(
                        name: "FK_utblAppointments_utblDepartment_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "utblDepartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblAppointments_utblDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "utblDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblAppointments_utblPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "utblPatients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblDoctorSchedule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblDoctorSchedule", x => x.Id);
                    table.CheckConstraint("CK_utblDoctorSchedule_EndTime_EndTimeShouldBeAfterStartTime", "[EndTime] > [StartTime]");
                    table.ForeignKey(
                        name: "FK_utblDoctorSchedule_utblDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "utblDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblInvoices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValueSql: "('Inv-' + Right('000000' + CAST(Next Value For InvoiceSeq AS VARCHAR(6)), 6))"),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_utblInvoices_utblAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "utblAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblInvoices_utblPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "utblPatients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblMedicalRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BloodPressure = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Pulse = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedById = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblMedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_utblMedicalRecords_utblAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "utblAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblMedicalRecords_utblDoctors_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "utblDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblPrescriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IssuedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblPrescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_utblPrescriptions_utblAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "utblAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblPrescriptions_utblDoctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "utblDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_utblPrescriptions_utblPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "utblPatients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "utblInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false, computedColumnSql: "[Quantity] * [UnitPrice]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblInvoiceItems", x => x.Id);
                    table.CheckConstraint("CK_utblInvoiceItem_Quantity_GreaterThanOrEqualTo1", "[Quantity] >= 1");
                    table.CheckConstraint("CK_utblInvoiceItem_UnitPrice_GreaterThanOrEqualTo0", "[UnitPrice] >= 0");
                    table.ForeignKey(
                        name: "FK_utblInvoiceItems_utblInvoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "utblInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "utblPrescriptionsItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrescriptionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dosage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utblPrescriptionsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_utblPrescriptionsItem_utblPrescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "utblPrescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_utblAppointments_DepartmentId",
                table: "utblAppointments",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_utblAppointments_DoctorId",
                table: "utblAppointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_utblAppointments_PatientId",
                table: "utblAppointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_utblDepartment_Name",
                table: "utblDepartment",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblDoctors_ApplicationUserId",
                table: "utblDoctors",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblDoctors_DepartmentId",
                table: "utblDoctors",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_utblDoctors_LicenseNumber",
                table: "utblDoctors",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblDoctorSchedule_DoctorId",
                table: "utblDoctorSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_utblInvoiceItems_InvoiceId",
                table: "utblInvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_utblInvoices_AppointmentId",
                table: "utblInvoices",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblInvoices_InvoiceNumber",
                table: "utblInvoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblInvoices_PatientId",
                table: "utblInvoices",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_utblMedicalRecords_AppointmentId",
                table: "utblMedicalRecords",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblMedicalRecords_CreatedById",
                table: "utblMedicalRecords",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_utblPatients_PatientNumber",
                table: "utblPatients",
                column: "PatientNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblPrescriptions_AppointmentId",
                table: "utblPrescriptions",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_utblPrescriptions_DoctorId",
                table: "utblPrescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_utblPrescriptions_PatientId",
                table: "utblPrescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_utblPrescriptionsItem_PrescriptionId",
                table: "utblPrescriptionsItem",
                column: "PrescriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "utblDoctorSchedule");

            migrationBuilder.DropTable(
                name: "utblInvoiceItems");

            migrationBuilder.DropTable(
                name: "utblMedicalRecords");

            migrationBuilder.DropTable(
                name: "utblPrescriptionsItem");

            migrationBuilder.DropTable(
                name: "utblInvoices");

            migrationBuilder.DropTable(
                name: "utblPrescriptions");

            migrationBuilder.DropTable(
                name: "utblAppointments");

            migrationBuilder.DropTable(
                name: "utblDoctors");

            migrationBuilder.DropTable(
                name: "utblPatients");

            migrationBuilder.DropTable(
                name: "utblApplicationUser");

            migrationBuilder.DropTable(
                name: "utblDepartment");

            migrationBuilder.DropSequence(
                name: "AppointmentSeq");

            migrationBuilder.DropSequence(
                name: "InvoiceSeq");

            migrationBuilder.DropSequence(
                name: "PatientSeq");
        }
    }
}
