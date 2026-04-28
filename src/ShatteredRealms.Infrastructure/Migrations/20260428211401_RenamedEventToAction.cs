using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamedEventtoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 463,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 562,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 566,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Events", "Events.Register" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 463,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Create" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Update" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Delete" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Register" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.ManageAttendees" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 562,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.View" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 566,
                columns: new[] { "Category", "ClaimValue" },
                values: new object[] { "Actions", "Actions.Register" });
        }
    }
}
