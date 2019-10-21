using Microsoft.EntityFrameworkCore.Migrations;

namespace Jp.Infra.Data.Sql.Migrations.EventStore
{
    public partial class BetterLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "RemoteIpAddress",
                table: "StoredEvent",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "StoredEvent");

            migrationBuilder.DropColumn(
                name: "LocalIpAddress",
                table: "StoredEvent");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "StoredEvent");

            migrationBuilder.DropColumn(
                name: "RemoteIpAddress",
                table: "StoredEvent");
        }
    }
}
