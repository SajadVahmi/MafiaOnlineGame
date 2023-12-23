using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Players.Persistence.SQL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PlayersSequence",
                minValue: 1L);

            migrationBuilder.CreateTable(
                name: "OutBoxEventItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccuredByUserId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AccuredOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AggregateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AggregateTypeName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EventName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EventTypeName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EventPayload = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutBoxEventItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    RegisterDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutBoxEventItems");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropSequence(
                name: "PlayersSequence");
        }
    }
}
