using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class MeniModelsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuSectionId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Href = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Target = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageStatus = table.Column<int>(type: "int", nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionItems_MenuSections_MenuSectionId",
                        column: x => x.MenuSectionId,
                        principalTable: "MenuSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuSectionItemId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Href = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageStatus = table.Column<int>(type: "int", nullable: false),
                    Target = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_SectionItems_MenuSectionItemId",
                        column: x => x.MenuSectionItemId,
                        principalTable: "SectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuSectionItemId",
                table: "MenuItems",
                column: "MenuSectionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionItems_MenuSectionId",
                table: "SectionItems",
                column: "MenuSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "SectionItems");

            migrationBuilder.DropTable(
                name: "MenuSections");
        }
    }
}
