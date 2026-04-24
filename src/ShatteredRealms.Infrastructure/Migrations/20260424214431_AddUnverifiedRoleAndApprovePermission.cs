using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnverifiedRoleAndApprovePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 161, "Users", "permission", "Users.Approve", "Approve a pending user registration", "00000000-0000-0000-0000-000000000001" },
                    { 261, "Users", "permission", "Users.Approve", "Approve a pending user registration", "00000000-0000-0000-0000-000000000002" },
                    { 557, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "IsSystem", "Name", "NormalizedName", "Priority" },
                values: new object[] { "00000000-0000-0000-0000-000000000007", "b1000000-0000-0000-0000-000000000007", "Newly registered user awaiting admin approval", false, "Unverified", "UNVERIFIED", 5 });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 602, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000007" },
                    { 604, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000007" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000007");
        }
    }
}
