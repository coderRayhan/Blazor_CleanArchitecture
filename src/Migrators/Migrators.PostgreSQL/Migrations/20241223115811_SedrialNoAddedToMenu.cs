using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class SedrialNoAddedToMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "serial_no",
                table: "menu_sections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "menu_section_sub_items",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "target",
                table: "menu_section_sub_items",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "page_status",
                table: "menu_section_sub_items",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "href",
                table: "menu_section_sub_items",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "serial_no",
                table: "menu_section_sub_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "serial_no",
                table: "menu_section_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "application_infos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    version_id = table.Column<int>(type: "integer", nullable: false),
                    branch_id = table.Column<int>(type: "integer", nullable: false),
                    shift_id = table.Column<int>(type: "integer", nullable: false),
                    class_id = table.Column<int>(type: "integer", nullable: false),
                    application_id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    password = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    student_type_id = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    full_name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    full_name_bangla = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    sms_notification_no = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    nationality = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    religion = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    father_name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    father_name_bangla = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    mother_name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    mother_name_bangla = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    photo_url = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    signature_url = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_infos", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_menu_section_sub_items_page_status",
                table: "menu_section_sub_items",
                column: "page_status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application_infos");

            migrationBuilder.DropIndex(
                name: "ix_menu_section_sub_items_page_status",
                table: "menu_section_sub_items");

            migrationBuilder.DropColumn(
                name: "serial_no",
                table: "menu_sections");

            migrationBuilder.DropColumn(
                name: "serial_no",
                table: "menu_section_sub_items");

            migrationBuilder.DropColumn(
                name: "serial_no",
                table: "menu_section_items");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "menu_section_sub_items",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "target",
                table: "menu_section_sub_items",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "page_status",
                table: "menu_section_sub_items",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "href",
                table: "menu_section_sub_items",
                type: "character varying(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
