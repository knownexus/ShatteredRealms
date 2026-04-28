using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TelemetryEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ActorEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TargetId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TargetName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Details = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryEvent", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 171, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000001" },
                    { 271, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000002" },
                    { 371, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000005" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvent_ActorId",
                table: "TelemetryEvent",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvent_EventType",
                table: "TelemetryEvent",
                column: "EventType");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvent_OccurredAt",
                table: "TelemetryEvent",
                column: "OccurredAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelemetryEvent");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 371);
        }
    }
}
