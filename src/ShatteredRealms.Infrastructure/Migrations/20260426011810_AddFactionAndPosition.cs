using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFactionAndPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Faction",
                table: "Character",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Character",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Position",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An ordinary member of society with no special rank", "Commoner" },
                    { 2, "A free man with basic rights, not bound to a lord", "Freeman" },
                    { 3, "A trader dealing in goods and services", "Merchant" },
                    { 4, "A skilled worker or artisan", "Craftsman" },
                    { 5, "A member of a religious order", "Monk" },
                    { 6, "A member of the clergy", "Priest" },
                    { 7, "A professional soldier in service to a lord", "Man-at-Arms" },
                    { 8, "A ranged combatant specialising in the bow", "Archer" },
                    { 9, "A mid-ranking military officer", "Sergeant" },
                    { 10, "A knight's attendant in training for knighthood", "Squire" },
                    { 11, "A mounted warrior granted a rank of honour", "Knight" },
                    { 12, "A commander of a military unit", "Captain" },
                    { 13, "An official responsible for managing an estate", "Steward" },
                    { 14, "A royal officer responsible for law and order", "Sheriff" },
                    { 15, "A member of the lowest order of the nobility", "Baron" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_PositionId",
                table: "Character",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_Position_PositionId",
                table: "Character",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_Position_PositionId",
                table: "Character");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Character_PositionId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "Faction",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Character");
        }
    }
}
