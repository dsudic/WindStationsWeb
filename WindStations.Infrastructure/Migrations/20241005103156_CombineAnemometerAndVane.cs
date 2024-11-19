using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WindStations.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CombineAnemometerAndVane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anemometer");

            migrationBuilder.DropTable(
                name: "Vane");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Environment",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Battery",
                newName: "Timestamp");

            migrationBuilder.CreateTable(
                name: "Wind",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinSpeed = table.Column<float>(type: "real", nullable: false),
                    AvgSpeed = table.Column<float>(type: "real", nullable: false),
                    MaxSpeed = table.Column<float>(type: "real", nullable: false),
                    Direction = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wind", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wind");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Environment",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Battery",
                newName: "TimeStamp");

            migrationBuilder.CreateTable(
                name: "Anemometer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgSpeed = table.Column<float>(type: "real", nullable: false),
                    MaxSpeed = table.Column<float>(type: "real", nullable: false),
                    MinSpeed = table.Column<float>(type: "real", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anemometer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgDirection = table.Column<float>(type: "real", nullable: false),
                    CompassDirection = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vane", x => x.Id);
                });
        }
    }
}
