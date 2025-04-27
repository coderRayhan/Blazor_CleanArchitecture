using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentWiseDesignationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationInfos");

            migrationBuilder.CreateTable(
                name: "DepartmentWiseDesignations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentWiseDesignations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentWiseDesignations_LookupDetails_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "LookupDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DepartmentWiseDesignations_LookupDetails_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "LookupDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentWiseDesignations_DepartmentId",
                table: "DepartmentWiseDesignations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentWiseDesignations_DesignationId",
                table: "DepartmentWiseDesignations",
                column: "DesignationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentWiseDesignations");

            migrationBuilder.CreateTable(
                name: "ApplicationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FatherNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MotherNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SMSNotificationNo = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    SignatureUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StudentTypeId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationInfos", x => x.Id);
                });
        }
    }
}
