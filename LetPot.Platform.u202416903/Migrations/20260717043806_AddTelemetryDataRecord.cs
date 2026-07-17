using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace LetPot.Platform.u202416903.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryDataRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_records",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    pot_mac_address = table.Column<string>(type: "longtext", nullable: false),
                    operation_mode = table.Column<int>(type: "int", nullable: false),
                    target_humidity_level = table.Column<double>(type: "double", nullable: false),
                    current_humidity_level = table.Column<double>(type: "double", nullable: false),
                    operation_phase = table.Column<int>(type: "int", nullable: false),
                    emitted_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_data_records", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_records");
        }
    }
}
