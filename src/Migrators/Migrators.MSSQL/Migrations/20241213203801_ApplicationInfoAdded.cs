using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationInfoAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StudentTypeId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FullNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SMSNotificationNo = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FatherNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MotherNameBangla = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    SignatureUrl = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationInfos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationInfos");
        }
    }
}
