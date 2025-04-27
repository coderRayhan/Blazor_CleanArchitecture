using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_roles_tenants_tenant_id",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_asp_net_users_superior_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_users_tenants_tenant_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "fk_documents_tenants_tenant_id",
                table: "documents");

            migrationBuilder.DropTable(
                name: "tenants");

            migrationBuilder.DropIndex(
                name: "ix_documents_tenant_id",
                table: "documents");

            migrationBuilder.DropIndex(
                name: "ix_asp_net_users_superior_id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "ix_asp_net_users_tenant_id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "ix_asp_net_roles_tenant_id_name",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "superior_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "tenant_id",
                table: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "documents",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "superior_id",
                table: "AspNetUsers",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "AspNetUsers",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenant_id",
                table: "AspNetRoles",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tenants",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tenants", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_documents_tenant_id",
                table: "documents",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_superior_id",
                table: "AspNetUsers",
                column: "superior_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_users_tenant_id",
                table: "AspNetUsers",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_roles_tenant_id_name",
                table: "AspNetRoles",
                columns: new[] { "tenant_id", "name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_roles_tenants_tenant_id",
                table: "AspNetRoles",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_asp_net_users_superior_id",
                table: "AspNetUsers",
                column: "superior_id",
                principalTable: "AspNetUsers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_users_tenants_tenant_id",
                table: "AspNetUsers",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_documents_tenants_tenant_id",
                table: "documents",
                column: "tenant_id",
                principalTable: "tenants",
                principalColumn: "id");
        }
    }
}
