using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatchService.Model.Migrations
{
    /// <inheritdoc />
    public partial class Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "driver",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passport = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    driver_license = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_plate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    vehicle_type = table.Column<int>(type: "integer", nullable: false),
                    model_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_low_floor = table.Column<bool>(type: "boolean", nullable: false),
                    max_capacity = table.Column<int>(type: "integer", nullable: false),
                    year_of_manufacture = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transport", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "route",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    route_number = table.Column<string>(type: "text", nullable: false),
                    assigned_transport = table.Column<int>(type: "integer", nullable: false),
                    assigned_driver = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_route", x => x.id);
                    table.ForeignKey(
                        name: "FK_route_driver_assigned_driver",
                        column: x => x.assigned_driver,
                        principalTable: "driver",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_route_transport_assigned_transport",
                        column: x => x.assigned_transport,
                        principalTable: "transport",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_route_assigned_driver",
                table: "route",
                column: "assigned_driver");

            migrationBuilder.CreateIndex(
                name: "IX_route_assigned_transport",
                table: "route",
                column: "assigned_transport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "route");

            migrationBuilder.DropTable(
                name: "driver");

            migrationBuilder.DropTable(
                name: "transport");
        }
    }
}
