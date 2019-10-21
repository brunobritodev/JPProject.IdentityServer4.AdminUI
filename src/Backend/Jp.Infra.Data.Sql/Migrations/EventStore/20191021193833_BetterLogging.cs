using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jp.Infra.Data.Sql.Migrations.EventStore
{
    public partial class BetterLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "StoredEvent",
                newName: "RemoteIpAddress");

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "StoredEvent",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocalIpAddress",
                table: "StoredEvent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "StoredEvent",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StoredEventDetails",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    Metadata = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEventDetails", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_StoredEventDetails_StoredEvent_EventId",
                        column: x => x.EventId,
                        principalTable: "StoredEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredEventDetails");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "StoredEvent");

            migrationBuilder.DropColumn(
                name: "LocalIpAddress",
                table: "StoredEvent");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "StoredEvent");

            migrationBuilder.RenameColumn(
                name: "RemoteIpAddress",
                table: "StoredEvent",
                newName: "Data");
        }
    }
}
