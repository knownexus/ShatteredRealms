using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityLogCharacterLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ActivityLog",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "ActivityLog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_CharacterId",
                table: "ActivityLog",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLog_Character_CharacterId",
                table: "ActivityLog",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLog_Character_CharacterId",
                table: "ActivityLog");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLog_CharacterId",
                table: "ActivityLog");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "ActivityLog");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ActivityLog",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024);
        }
    }
}
