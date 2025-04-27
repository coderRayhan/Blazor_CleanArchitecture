using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.CreateTable(
                name: "menu_sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    roles = table.Column<string[]>(type: "text[]", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_sections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "versions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    version_code = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_versions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu_section_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_section_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    icon = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    href = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    target = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    roles = table.Column<string[]>(type: "text[]", nullable: true),
                    page_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_parent = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_section_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_section_items_menu_sections_menu_section_id",
                        column: x => x.menu_section_id,
                        principalTable: "menu_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menu_section_sub_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    menu_section_item_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    href = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    roles = table.Column<string[]>(type: "text[]", nullable: true),
                    page_status = table.Column<int>(type: "integer", nullable: false),
                    target = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_menu_section_sub_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_menu_section_sub_items_menu_section_items_menu_section_item",
                        column: x => x.menu_section_item_id,
                        principalTable: "menu_section_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_menu_section_items_menu_section_id",
                table: "menu_section_items",
                column: "menu_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_menu_section_items_page_status",
                table: "menu_section_items",
                column: "page_status");

            migrationBuilder.CreateIndex(
                name: "ix_menu_section_sub_items_menu_section_item_id",
                table: "menu_section_sub_items",
                column: "menu_section_item_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu_section_sub_items");

            migrationBuilder.DropTable(
                name: "versions");

            migrationBuilder.DropTable(
                name: "menu_section_items");

            migrationBuilder.DropTable(
                name: "menu_sections");

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    email = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    last_modified_by = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    content = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    document_type = table.Column<string>(type: "text", nullable: false),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    url = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_documents_asp_net_users_created_by",
                        column: x => x.created_by,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_documents_asp_net_users_last_modified_by",
                        column: x => x.last_modified_by,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_documents_created_by",
                table: "documents",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "ix_documents_last_modified_by",
                table: "documents",
                column: "last_modified_by");
        }
    }
}
