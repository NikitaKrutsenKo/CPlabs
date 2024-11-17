using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab6.Migrations
{
    public partial class MSSQLmigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Address_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherAddressDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Address_ID);
                });

            migrationBuilder.CreateTable(
                name: "RefTimeOffReasons",
                columns: table => new
                {
                    ReasonCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReasonDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTimeOffReasons", x => x.ReasonCode);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Shift_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOrNight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OtherShiftDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Shift_ID);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    Ward_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.Ward_ID);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Hospital_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_ID = table.Column<int>(type: "int", nullable: false),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Hospital_ID);
                    table.ForeignKey(
                        name: "FK_Hospitals_Addresses_Address_ID",
                        column: x => x.Address_ID,
                        principalTable: "Addresses",
                        principalColumn: "Address_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Staff_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address_ID = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateJoinedHospital = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLeftHospital = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hospital_ID = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qualifications = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Staff_ID);
                    table.ForeignKey(
                        name: "FK_Staff_Addresses_Address_ID",
                        column: x => x.Address_ID,
                        principalTable: "Addresses",
                        principalColumn: "Address_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Hospitals_Hospital_ID",
                        column: x => x.Hospital_ID,
                        principalTable: "Hospitals",
                        principalColumn: "Hospital_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RosterOfStaffOnShifts",
                columns: table => new
                {
                    Roster_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    Shift_ID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RosterOfStaffOnShifts", x => x.Roster_ID);
                    table.ForeignKey(
                        name: "FK_RosterOfStaffOnShifts_Shifts_Shift_ID",
                        column: x => x.Shift_ID,
                        principalTable: "Shifts",
                        principalColumn: "Shift_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RosterOfStaffOnShifts_Staff_Staff_ID",
                        column: x => x.Staff_ID,
                        principalTable: "Staff",
                        principalColumn: "Staff_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffPay",
                columns: table => new
                {
                    Pay_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GrossPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffPay", x => x.Pay_ID);
                    table.ForeignKey(
                        name: "FK_StaffPay_Staff_Staff_ID",
                        column: x => x.Staff_ID,
                        principalTable: "Staff",
                        principalColumn: "Staff_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffTimeOffs",
                columns: table => new
                {
                    StaffTimeOff_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    ReasonCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffTimeOffs", x => x.StaffTimeOff_ID);
                    table.ForeignKey(
                        name: "FK_StaffTimeOffs_RefTimeOffReasons_ReasonCode",
                        column: x => x.ReasonCode,
                        principalTable: "RefTimeOffReasons",
                        principalColumn: "ReasonCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffTimeOffs_Staff_Staff_ID",
                        column: x => x.Staff_ID,
                        principalTable: "Staff",
                        principalColumn: "Staff_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StaffWardAssignments",
                columns: table => new
                {
                    Assignment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff_ID = table.Column<int>(type: "int", nullable: false),
                    Ward_ID = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffWardAssignments", x => x.Assignment_ID);
                    table.ForeignKey(
                        name: "FK_StaffWardAssignments_Staff_Staff_ID",
                        column: x => x.Staff_ID,
                        principalTable: "Staff",
                        principalColumn: "Staff_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffWardAssignments_Wards_Ward_ID",
                        column: x => x.Ward_ID,
                        principalTable: "Wards",
                        principalColumn: "Ward_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_Address_ID",
                table: "Hospitals",
                column: "Address_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterOfStaffOnShifts_Shift_ID",
                table: "RosterOfStaffOnShifts",
                column: "Shift_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RosterOfStaffOnShifts_Staff_ID",
                table: "RosterOfStaffOnShifts",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Address_ID",
                table: "Staff",
                column: "Address_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Hospital_ID",
                table: "Staff",
                column: "Hospital_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffPay_Staff_ID",
                table: "StaffPay",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTimeOffs_ReasonCode",
                table: "StaffTimeOffs",
                column: "ReasonCode");

            migrationBuilder.CreateIndex(
                name: "IX_StaffTimeOffs_Staff_ID",
                table: "StaffTimeOffs",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffWardAssignments_Staff_ID",
                table: "StaffWardAssignments",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_StaffWardAssignments_Ward_ID",
                table: "StaffWardAssignments",
                column: "Ward_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RosterOfStaffOnShifts");

            migrationBuilder.DropTable(
                name: "StaffPay");

            migrationBuilder.DropTable(
                name: "StaffTimeOffs");

            migrationBuilder.DropTable(
                name: "StaffWardAssignments");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "RefTimeOffReasons");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
