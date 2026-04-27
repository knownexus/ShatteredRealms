using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryFlagging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActorName",
                table: "TelemetryEvent",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActorRole",
                table: "TelemetryEvent",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FlagReason",
                table: "TelemetryEvent",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FlaggedAt",
                table: "TelemetryEvent",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlaggedById",
                table: "TelemetryEvent",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFlagged",
                table: "TelemetryEvent",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AnalyticsFlagRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleType = table.Column<int>(type: "int", nullable: false),
                    TargetUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    EventType = table.Column<int>(type: "int", nullable: true),
                    ActorRole = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticsFlagRule", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvent_ActorName",
                table: "TelemetryEvent",
                column: "ActorName");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvent_IsFlagged",
                table: "TelemetryEvent",
                column: "IsFlagged");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsFlagRule_IsActive",
                table: "AnalyticsFlagRule",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsFlagRule_RuleType",
                table: "AnalyticsFlagRule",
                column: "RuleType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticsFlagRule");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryEvent_ActorName",
                table: "TelemetryEvent");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryEvent_IsFlagged",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "ActorName",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "ActorRole",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "FlagReason",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "FlaggedAt",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "FlaggedById",
                table: "TelemetryEvent");

            migrationBuilder.DropColumn(
                name: "IsFlagged",
                table: "TelemetryEvent");
        }
    }
}
