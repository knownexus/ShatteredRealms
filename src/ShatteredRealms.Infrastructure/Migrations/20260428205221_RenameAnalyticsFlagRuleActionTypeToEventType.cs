using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAnalyticsFlagRuleActionTypeToEventType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventType",
                table: "TelemetryEvent",
                newName: "ActionType");

            migrationBuilder.RenameIndex(
                name: "IX_TelemetryEvent_EventType",
                table: "TelemetryEvent",
                newName: "IX_TelemetryEvent_ActionType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActionType",
                table: "TelemetryEvent",
                newName: "EventType");

            migrationBuilder.RenameIndex(
                name: "IX_TelemetryEvent_ActionType",
                table: "TelemetryEvent",
                newName: "IX_TelemetryEvent_EventType");
        }
    }
}
