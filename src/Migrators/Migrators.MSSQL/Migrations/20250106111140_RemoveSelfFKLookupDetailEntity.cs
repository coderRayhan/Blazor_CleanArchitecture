using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSelfFKLookupDetailEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LookupDetails_LookupDetails_DetailId",
                table: "LookupDetails");

            migrationBuilder.DropIndex(
                name: "IX_LookupDetails_DetailId",
                table: "LookupDetails");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "LookupDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "LookupDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LookupDetails_DetailId",
                table: "LookupDetails",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_LookupDetails_LookupDetails_DetailId",
                table: "LookupDetails",
                column: "DetailId",
                principalTable: "LookupDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
