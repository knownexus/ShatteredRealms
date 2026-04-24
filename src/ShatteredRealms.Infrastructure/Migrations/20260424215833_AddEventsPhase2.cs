using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEventsPhase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    BannerImagePath = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    MemberCap = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventAttendee",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendee", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EventAttendee_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventAttendee_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 162, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000001" },
                    { 163, "Events", "permission", "Events.Create", "Create events", "00000000-0000-0000-0000-000000000001" },
                    { 164, "Events", "permission", "Events.Update", "Edit any event", "00000000-0000-0000-0000-000000000001" },
                    { 165, "Events", "permission", "Events.Delete", "Delete any event", "00000000-0000-0000-0000-000000000001" },
                    { 166, "Events", "permission", "Events.Register", "Mark self as going to an event", "00000000-0000-0000-0000-000000000001" },
                    { 167, "Events", "permission", "Events.ManageAttendees", "Remove attendees from an event", "00000000-0000-0000-0000-000000000001" },
                    { 262, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000002" },
                    { 263, "Events", "permission", "Events.Create", "Create events", "00000000-0000-0000-0000-000000000002" },
                    { 264, "Events", "permission", "Events.Update", "Edit any event", "00000000-0000-0000-0000-000000000002" },
                    { 265, "Events", "permission", "Events.Delete", "Delete any event", "00000000-0000-0000-0000-000000000002" },
                    { 266, "Events", "permission", "Events.Register", "Mark self as going to an event", "00000000-0000-0000-0000-000000000002" },
                    { 267, "Events", "permission", "Events.ManageAttendees", "Remove attendees from an event", "00000000-0000-0000-0000-000000000002" },
                    { 462, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000003" },
                    { 463, "Events", "permission", "Events.Create", "Create events", "00000000-0000-0000-0000-000000000003" },
                    { 464, "Events", "permission", "Events.Update", "Edit any event", "00000000-0000-0000-0000-000000000003" },
                    { 465, "Events", "permission", "Events.Delete", "Delete any event", "00000000-0000-0000-0000-000000000003" },
                    { 466, "Events", "permission", "Events.Register", "Mark self as going to an event", "00000000-0000-0000-0000-000000000003" },
                    { 467, "Events", "permission", "Events.ManageAttendees", "Remove attendees from an event", "00000000-0000-0000-0000-000000000003" },
                    { 562, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000004" },
                    { 566, "Events", "permission", "Events.Register", "Mark self as going to an event", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_CreatedById",
                table: "Event",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EventAttendee_UserId",
                table: "EventAttendee",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventAttendee");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 566);
        }
    }
}
