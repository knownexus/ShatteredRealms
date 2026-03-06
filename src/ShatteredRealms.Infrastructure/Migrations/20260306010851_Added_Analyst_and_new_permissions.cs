using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Analyst_and_new_permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLog_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.Create", "Create users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.ViewOwn", "View own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.UpdateOwn", "Update own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.CreateOwn", "Create own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Create", "Create character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.ViewOwn", "View own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.UpdateOwn", "Update own character name/nation" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Update", "Update any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Delete", "Delete any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.DeleteOwn", "Delete own character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignPosition", "Assign societal/military position" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignExperience", "Assign experience/level" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Update", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.UpdateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135,
                column: "ClaimValue",
                value: "Forum.Post.UpdateOwn");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Update", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Update", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.Create", "Create users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.ViewOwn", "View own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.UpdateOwn", "Update own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.CreateOwn", "Create own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Create", "Create character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.ViewOwn", "View own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.UpdateOwn", "Update own character name/nation" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Update", "Update any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Delete", "Delete any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.DeleteOwn", "Delete own character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignPosition", "Assign societal/military position" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignExperience", "Assign experience/level" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Update", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.UpdateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 235,
                column: "ClaimValue",
                value: "Forum.Post.UpdateOwn");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Update", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Update", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "Forum.Post.UpdateOwn", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "Category", "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Wiki.Page.Create", "Create wiki pages", "00000000-0000-0000-0000-000000000003" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 143, "Wiki", "permission", "Wiki.Page.Delete", "Delete wiki pages", "00000000-0000-0000-0000-000000000001" },
                    { 144, "Wiki", "permission", "Wiki.Category.Manage", "Create/update wiki categories", "00000000-0000-0000-0000-000000000001" },
                    { 145, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000001" },
                    { 146, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000001" },
                    { 147, "Videos", "permission", "Videos.Update", "Update video details", "00000000-0000-0000-0000-000000000001" },
                    { 148, "Videos", "permission", "Videos.Delete", "Delete video", "00000000-0000-0000-0000-000000000001" },
                    { 149, "Videos", "permission", "Videos.DeleteOwn", "Delete own video", "00000000-0000-0000-0000-000000000001" },
                    { 150, "Videos", "permission", "Videos.Approve", "Approve video", "00000000-0000-0000-0000-000000000001" },
                    { 151, "ActivityLogs", "permission", "ActivityLog.View", "View activity logs", "00000000-0000-0000-0000-000000000001" },
                    { 152, "ActivityLogs", "permission", "ActivityLog.Update", "Edit activity logs", "00000000-0000-0000-0000-000000000001" },
                    { 153, "ActivityLogs", "permission", "ActivityLog.Delete", "Delete activity logs", "00000000-0000-0000-0000-000000000001" },
                    { 154, "ActivityLogs", "permission", "Reports.View", "View Reports", "00000000-0000-0000-0000-000000000001" },
                    { 155, "ActivityLogs", "permission", "Reports.Create", "Generate specific report", "00000000-0000-0000-0000-000000000001" },
                    { 156, "ActivityLogs", "permission", "Reports.CreateAll", "Generate all reports", "00000000-0000-0000-0000-000000000001" },
                    { 157, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000001" },
                    { 158, "Announcements", "permission", "Announcements.Create", "Create a new announcements", "00000000-0000-0000-0000-000000000001" },
                    { 159, "Announcements", "permission", "Announcements.Update", "Edit an announcement", "00000000-0000-0000-0000-000000000001" },
                    { 160, "Announcements", "permission", "Announcements.Delete", "Delete an announcement", "00000000-0000-0000-0000-000000000001" },
                    { 243, "Wiki", "permission", "Wiki.Page.Delete", "Delete wiki pages", "00000000-0000-0000-0000-000000000002" },
                    { 244, "Wiki", "permission", "Wiki.Category.Manage", "Create/update wiki categories", "00000000-0000-0000-0000-000000000002" },
                    { 245, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000002" },
                    { 246, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000002" },
                    { 247, "Videos", "permission", "Videos.Update", "Update video details", "00000000-0000-0000-0000-000000000002" },
                    { 248, "Videos", "permission", "Videos.Delete", "Delete video", "00000000-0000-0000-0000-000000000002" },
                    { 249, "Videos", "permission", "Videos.DeleteOwn", "Delete own video", "00000000-0000-0000-0000-000000000002" },
                    { 250, "Videos", "permission", "Videos.Approve", "Approve video", "00000000-0000-0000-0000-000000000002" },
                    { 251, "ActivityLogs", "permission", "ActivityLog.View", "View activity logs", "00000000-0000-0000-0000-000000000002" },
                    { 252, "ActivityLogs", "permission", "ActivityLog.Update", "Edit activity logs", "00000000-0000-0000-0000-000000000002" },
                    { 253, "ActivityLogs", "permission", "ActivityLog.Delete", "Delete activity logs", "00000000-0000-0000-0000-000000000002" },
                    { 254, "ActivityLogs", "permission", "Reports.View", "View Reports", "00000000-0000-0000-0000-000000000002" },
                    { 255, "ActivityLogs", "permission", "Reports.Create", "Generate specific report", "00000000-0000-0000-0000-000000000002" },
                    { 256, "ActivityLogs", "permission", "Reports.CreateAll", "Generate all reports", "00000000-0000-0000-0000-000000000002" },
                    { 257, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000002" },
                    { 258, "Announcements", "permission", "Announcements.Create", "Create a new announcements", "00000000-0000-0000-0000-000000000002" },
                    { 259, "Announcements", "permission", "Announcements.Update", "Edit an announcement", "00000000-0000-0000-0000-000000000002" },
                    { 260, "Announcements", "permission", "Announcements.Delete", "Delete an announcement", "00000000-0000-0000-0000-000000000002" },
                    { 404, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000003" },
                    { 410, "Characters", "permission", "Characters.View", "View any character", "00000000-0000-0000-0000-000000000003" },
                    { 411, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000003" },
                    { 412, "Characters", "permission", "Characters.Update", "Update any character", "00000000-0000-0000-0000-000000000003" },
                    { 414, "Characters", "permission", "Characters.DeleteOwn", "Delete own character", "00000000-0000-0000-0000-000000000003" },
                    { 415, "Characters", "permission", "Characters.AssignPosition", "Assign societal/military position", "00000000-0000-0000-0000-000000000003" },
                    { 416, "Characters", "permission", "Characters.AssignExperience", "Assign experience/level", "00000000-0000-0000-0000-000000000003" },
                    { 427, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 428, "Forum", "permission", "Forum.Thread.UpdateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000003" },
                    { 432, "Forum", "permission", "Forum.Thread.Lock", "Lock/unlock forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 433, "Forum", "permission", "Forum.Thread.Pin", "Pin/unpin forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 434, "Forum", "permission", "Forum.Post.Create", "Create posts in threads", "00000000-0000-0000-0000-000000000003" },
                    { 440, "Wiki", "permission", "Wiki.Page.UpdateOwn", "Update own wiki page", "00000000-0000-0000-0000-000000000003" },
                    { 442, "Wiki", "permission", "Wiki.Page.DeleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000003" },
                    { 445, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000003" },
                    { 446, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000003" },
                    { 447, "Videos", "permission", "Videos.Update", "Update video details", "00000000-0000-0000-0000-000000000003" },
                    { 448, "Videos", "permission", "Videos.Delete", "Delete video", "00000000-0000-0000-0000-000000000003" },
                    { 450, "Videos", "permission", "Videos.Approve", "Approve video", "00000000-0000-0000-0000-000000000003" },
                    { 457, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000003" },
                    { 458, "Announcements", "permission", "Announcements.Create", "Create a new announcements", "00000000-0000-0000-0000-000000000003" },
                    { 459, "Announcements", "permission", "Announcements.Update", "Edit an announcement", "00000000-0000-0000-0000-000000000003" },
                    { 460, "Announcements", "permission", "Announcements.Delete", "Delete an announcement", "00000000-0000-0000-0000-000000000003" },
                    { 502, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000004" },
                    { 504, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000004" },
                    { 507, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000004" },
                    { 509, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000004" },
                    { 511, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000004" },
                    { 514, "Characters", "permission", "Characters.DeleteOwn", "Delete own character", "00000000-0000-0000-0000-000000000004" },
                    { 527, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000004" },
                    { 528, "Forum", "permission", "Forum.Thread.UpdateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000004" },
                    { 534, "Forum", "permission", "Forum.Post.Create", "Create posts in threads", "00000000-0000-0000-0000-000000000004" },
                    { 535, "Forum", "permission", "Forum.Post.UpdateOwn", "Update own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 537, "Forum", "permission", "Forum.Post.DeleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 539, "Wiki", "permission", "Wiki.Page.Create", "Create wiki pages", "00000000-0000-0000-0000-000000000004" },
                    { 540, "Wiki", "permission", "Wiki.Page.UpdateOwn", "Update own wiki page", "00000000-0000-0000-0000-000000000004" },
                    { 542, "Wiki", "permission", "Wiki.Page.DeleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000004" },
                    { 545, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000004" },
                    { 546, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000004" },
                    { 549, "Videos", "permission", "Videos.DeleteOwn", "Delete own video", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                column: "IsSystem",
                value: false);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                column: "Description",
                value: "Can manage events, characters, assign positions/experience");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "IsSystem", "Name", "NormalizedName", "Priority" },
                values: new object[] { "00000000-0000-0000-0000-000000000005", null, "Report Access", false, "Analyst", "ANALYST", 80 });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 354, "ActivityLogs", "permission", "Reports.View", "View Reports", "00000000-0000-0000-0000-000000000005" },
                    { 355, "ActivityLogs", "permission", "Reports.Create", "Generate specific report", "00000000-0000-0000-0000-000000000005" },
                    { 356, "ActivityLogs", "permission", "Reports.CreateAll", "Generate all reports", "00000000-0000-0000-0000-000000000005" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLog_UserId",
                table: "ActivityLog",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000005");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.ViewOwn", "View own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.UpdateOwn", "Update own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.Create", "Create users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.ViewOwn", "View own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.CreateOwn", "Create own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.UpdateOwnBasic", "Update own character name/nation" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Update", "Update any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Delete", "Delete any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.AssignPosition", "Assign societal/military position" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.AssignExperience", "Assign experience/level" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "forum.category.create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "forum.category.edit", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.category.delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.edit", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.updateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.deleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135,
                column: "ClaimValue",
                value: "forum.post.editOwn");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.deleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "wiki.page.create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "wiki.page.edit", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.editOwn", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.deleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.category.manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 201,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.ViewOwn", "View own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 202,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.UpdateOwn", "Update own profile" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 204,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Users.Create", "Create users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 207,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.ViewOwn", "View own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 208,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.CreateOwn", "Create own characters" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 209,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.UpdateOwnBasic", "Update own character name/nation" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 211,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Update", "Update any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 212,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.Delete", "Delete any character" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 213,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.AssignPosition", "Assign societal/military position" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 214,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Characters.AssignExperience", "Assign experience/level" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 215,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 216,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "forum.category.create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "forum.category.edit", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.category.delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.edit", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.updateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.thread.deleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 235,
                column: "ClaimValue",
                value: "forum.post.editOwn");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "forum.post.deleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "wiki.page.create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "wiki.page.edit", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.editOwn", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.page.deleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "wiki.category.manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 402,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 407,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 409,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Characters.UpdateOwnBasic", "Update own character name/nation", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "forum.post.create", "Create posts in threads", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "ClaimValue", "RoleId" },
                values: new object[] { "forum.post.editOwn", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "Category", "ClaimValue", "Description", "RoleId" },
                values: new object[] { "Wiki", "wiki.page.create", "Create wiki pages", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "ClaimValue", "Description", "RoleId" },
                values: new object[] { "wiki.page.editOwn", "Update any wiki page", "00000000-0000-0000-0000-000000000004" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 301, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000003" },
                    { 302, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000003" },
                    { 307, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000003" },
                    { 308, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000003" },
                    { 309, "Characters", "permission", "Characters.UpdateOwnBasic", "Update own character name/nation", "00000000-0000-0000-0000-000000000003" },
                    { 310, "Characters", "permission", "Characters.View", "View any character", "00000000-0000-0000-0000-000000000003" },
                    { 311, "Characters", "permission", "Characters.Update", "Update any character", "00000000-0000-0000-0000-000000000003" },
                    { 313, "Characters", "permission", "Characters.AssignPosition", "Assign societal/military position", "00000000-0000-0000-0000-000000000003" },
                    { 314, "Characters", "permission", "Characters.AssignExperience", "Assign experience/level", "00000000-0000-0000-0000-000000000003" },
                    { 315, "Role", "permission", "Role.View", "View roles", "00000000-0000-0000-0000-000000000003" },
                    { 320, "PermissionControl", "permission", "PermissionControl.View", "View permissions", "00000000-0000-0000-0000-000000000003" },
                    { 325, "Forum", "permission", "forum.thread.create", "Create forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 328, "Forum", "permission", "forum.thread.lock", "Lock/unlock forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 329, "Forum", "permission", "forum.thread.pin", "Pin/unpin forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 330, "Forum", "permission", "forum.post.create", "Create posts in threads", "00000000-0000-0000-0000-000000000003" },
                    { 333, "Forum", "permission", "forum.thread.updateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000003" },
                    { 334, "Forum", "permission", "forum.thread.deleteOwn", "Delete own forum thread", "00000000-0000-0000-0000-000000000003" },
                    { 335, "Forum", "permission", "forum.post.editOwn", "Update own forum post", "00000000-0000-0000-0000-000000000003" },
                    { 336, "Forum", "permission", "forum.post.deleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000003" },
                    { 337, "Wiki", "permission", "wiki.page.create", "Create wiki pages", "00000000-0000-0000-0000-000000000003" },
                    { 339, "Wiki", "permission", "wiki.page.editOwn", "Update any wiki page", "00000000-0000-0000-0000-000000000003" },
                    { 341, "Wiki", "permission", "wiki.page.deleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000003" },
                    { 401, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000004" },
                    { 408, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000004" },
                    { 436, "Forum", "permission", "forum.post.deleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 441, "Wiki", "permission", "wiki.page.deleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000002",
                column: "IsSystem",
                value: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000003",
                column: "Description",
                value: "Can manage characters, assign positions/experience");
        }
    }
}
