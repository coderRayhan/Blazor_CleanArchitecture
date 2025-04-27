using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class MenuModelsTableNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_SectionItems_MenuSectionItemId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SectionItems_MenuSections_MenuSectionId",
                table: "SectionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionItems",
                table: "SectionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "SectionItems",
                newName: "MenuSectionItems");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuSectionSubItems");

            migrationBuilder.RenameIndex(
                name: "IX_SectionItems_MenuSectionId",
                table: "MenuSectionItems",
                newName: "IX_MenuSectionItems_MenuSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuSectionItemId",
                table: "MenuSectionSubItems",
                newName: "IX_MenuSectionSubItems_MenuSectionItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSectionItems",
                table: "MenuSectionItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuSectionSubItems",
                table: "MenuSectionSubItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSectionItems_MenuSections_MenuSectionId",
                table: "MenuSectionItems",
                column: "MenuSectionId",
                principalTable: "MenuSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuSectionSubItems_MenuSectionItems_MenuSectionItemId",
                table: "MenuSectionSubItems",
                column: "MenuSectionItemId",
                principalTable: "MenuSectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuSectionItems_MenuSections_MenuSectionId",
                table: "MenuSectionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuSectionSubItems_MenuSectionItems_MenuSectionItemId",
                table: "MenuSectionSubItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSectionSubItems",
                table: "MenuSectionSubItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuSectionItems",
                table: "MenuSectionItems");

            migrationBuilder.RenameTable(
                name: "MenuSectionSubItems",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "MenuSectionItems",
                newName: "SectionItems");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSectionSubItems_MenuSectionItemId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuSectionItemId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuSectionItems_MenuSectionId",
                table: "SectionItems",
                newName: "IX_SectionItems_MenuSectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionItems",
                table: "SectionItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_SectionItems_MenuSectionItemId",
                table: "MenuItems",
                column: "MenuSectionItemId",
                principalTable: "SectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SectionItems_MenuSections_MenuSectionId",
                table: "SectionItems",
                column: "MenuSectionId",
                principalTable: "MenuSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
