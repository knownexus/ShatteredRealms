using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNationalitiesAndFactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 371);

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
                keyValue: 433);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 440);

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
                keyValue: 457);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 463);

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
                keyValue: 540);

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
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignNationality", "Assign character nationality" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignFaction", "Assign character faction" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Update", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.UpdateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Update", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Update", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Category.Manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.View", "View video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Create", "Upload video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Update", "Update video details" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.DeleteOwn", "Delete own video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.Approve", "Approve video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.View", "View activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Update", "Edit activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Delete", "Delete activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.View", "View Reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "Reports.Create", "Generate specific report" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "Reports.CreateAll", "Generate all reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.View", "View announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Create", "Create a new announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Update", "Edit an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Users", "Users.Approve", "Approve a pending user registration" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Create", "Create events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.Upload", "Upload documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignNationality", "Assign character nationality" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Characters", "Characters.AssignFaction", "Assign character faction" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 219,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 220,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.View", "View roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Update", "Update roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 222,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 223,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Assign", "Assign roles to users" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 224,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.View", "View permissions" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "PermissionControl", "PermissionControl.Assign", "Assign permissions to roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 226,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 227,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Update", "Update forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 228,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Category.Delete", "Delete forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 229,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Create", "Create forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 230,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.UpdateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 231,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Update", "Update any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 232,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 233,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Delete", "Delete any forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 234,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 235,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 236,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 237,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Update", "Update any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 241,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 242,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 243,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Update", "Update any wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 244,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 245,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 246,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Category.Manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 247,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.View", "View video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 248,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Create", "Upload video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 249,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Update", "Update video details" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 250,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 251,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.DeleteOwn", "Delete own video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 252,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.Approve", "Approve video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 253,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.View", "View activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 254,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Update", "Edit activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 255,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Delete", "Delete activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 256,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.View", "View Reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 257,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "Reports.Create", "Generate specific report" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 258,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "Reports.CreateAll", "Generate all reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 259,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.View", "View announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 260,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Create", "Create a new announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 261,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Update", "Edit an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Users", "Users.Approve", "Approve a pending user registration" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Create", "Create events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 268,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 269,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 270,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 271,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.Upload", "Upload documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 356,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.View", "View Reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.UpdateOwn", "Update own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 432,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 434,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Pin", "Pin/unpin forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 442,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 447,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.View", "View video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 448,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Create", "Upload video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 450,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 459,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.View", "View announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 460,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Create", "Create a new announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Create", "Create events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 468,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 469,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 470,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 537,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 539,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 542,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.UpdateOwn", "Update own wiki page" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 568,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Register", "Mark self as going to an event" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 172, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000001" },
                    { 173, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000001" },
                    { 272, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000002" },
                    { 273, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000002" },
                    { 357, "ActivityLogs", "permission", "Reports.Create", "Generate specific report", "00000000-0000-0000-0000-000000000005" },
                    { 358, "ActivityLogs", "permission", "Reports.CreateAll", "Generate all reports", "00000000-0000-0000-0000-000000000005" },
                    { 373, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000005" },
                    { 417, "Characters", "permission", "Characters.AssignNationality", "Assign character nationality", "00000000-0000-0000-0000-000000000003" },
                    { 418, "Characters", "permission", "Characters.AssignFaction", "Assign character faction", "00000000-0000-0000-0000-000000000003" },
                    { 429, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 436, "Forum", "permission", "Forum.Post.Create", "Create posts in threads", "00000000-0000-0000-0000-000000000003" },
                    { 441, "Wiki", "permission", "Wiki.Page.Create", "Create wiki pages", "00000000-0000-0000-0000-000000000003" },
                    { 444, "Wiki", "permission", "Wiki.Page.DeleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000003" },
                    { 449, "Videos", "permission", "Videos.Update", "Update video details", "00000000-0000-0000-0000-000000000003" },
                    { 452, "Videos", "permission", "Videos.Approve", "Approve video", "00000000-0000-0000-0000-000000000003" },
                    { 461, "Announcements", "permission", "Announcements.Update", "Edit an announcement", "00000000-0000-0000-0000-000000000003" },
                    { 471, "Documents", "permission", "Documents.Upload", "Upload documents", "00000000-0000-0000-0000-000000000003" },
                    { 472, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000003" },
                    { 529, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000004" },
                    { 530, "Forum", "permission", "Forum.Thread.UpdateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000004" },
                    { 536, "Forum", "permission", "Forum.Post.Create", "Create posts in threads", "00000000-0000-0000-0000-000000000004" },
                    { 541, "Wiki", "permission", "Wiki.Page.Create", "Create wiki pages", "00000000-0000-0000-0000-000000000004" },
                    { 544, "Wiki", "permission", "Wiki.Page.DeleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000004" },
                    { 547, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000004" },
                    { 548, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000004" },
                    { 551, "Videos", "permission", "Videos.DeleteOwn", "Delete own video", "00000000-0000-0000-0000-000000000004" },
                    { 559, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000004" },
                    { 564, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000004" },
                    { 570, "Documents", "permission", "Documents.View", "View and download documents", "00000000-0000-0000-0000-000000000004" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.View", "View roles" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Assign", "Assign roles to users" });

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
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Category.Update", "Update forum categories" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.UpdateOwn", "Update own wiki page" });

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
                keyValue: 143,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Category.Manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.View", "View video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.Create", "Upload video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Update", "Update video details" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.DeleteOwn", "Delete own video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Approve", "Approve video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 151,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "ActivityLog.View", "View activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 152,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "ActivityLog.Update", "Edit activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 153,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Delete", "Delete activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 154,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.View", "View Reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 155,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.Create", "Generate specific report" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 156,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.CreateAll", "Generate all reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 157,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.View", "View announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 158,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Create", "Create a new announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 159,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Update", "Edit an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 160,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 161,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Users", "Users.Approve", "Approve a pending user registration" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 162,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 163,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Create", "Create events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 164,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 165,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 166,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 167,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.Upload", "Upload documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.Delete", "Delete documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 171,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Analytics", "Analytics.View", "View analytics and telemetry" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 217,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.Create", "Create roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 218,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Role", "Role.View", "View roles" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Delete", "Delete roles" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 221,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Role.Assign", "Assign roles to users" });

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
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Category.Create", "Create forum categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 225,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Forum", "Forum.Category.Update", "Update forum categories" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

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
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 238,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Delete", "Delete any forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 239,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 240,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.UpdateOwn", "Update own wiki page" });

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
                keyValue: 243,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.Delete", "Delete wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 244,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Category.Manage", "Create/update wiki categories" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 245,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.View", "View video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 246,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Videos", "Videos.Create", "Upload video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 247,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Update", "Update video details" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 248,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 249,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.DeleteOwn", "Delete own video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 250,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Approve", "Approve video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 251,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "ActivityLog.View", "View activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 252,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "ActivityLogs", "ActivityLog.Update", "Edit activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 253,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "ActivityLog.Delete", "Delete activity logs" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 254,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.View", "View Reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 255,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.Create", "Generate specific report" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 256,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.CreateAll", "Generate all reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 257,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.View", "View announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 258,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Announcements", "Announcements.Create", "Create a new announcements" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 259,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Update", "Edit an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 260,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 261,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Users", "Users.Approve", "Approve a pending user registration" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 262,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 263,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.Create", "Create events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 264,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 265,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 266,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 267,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 268,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 269,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.Upload", "Upload documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 270,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.Delete", "Delete documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 271,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Analytics", "Analytics.View", "View analytics and telemetry" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 356,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Reports.CreateAll", "Generate all reports" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 430,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.DeleteOwn", "Delete own forum thread" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 432,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Thread.Lock", "Lock/unlock forum threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 434,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.Create", "Create posts in threads" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 435,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.UpdateOwn", "Update own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 437,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 439,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 442,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 447,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Update", "Update video details" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 448,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Delete", "Delete video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 450,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Videos.Approve", "Approve video" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 459,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Update", "Edit an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 460,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Announcements.Delete", "Delete an announcement" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 462,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Events", "Events.View", "View events" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 464,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Update", "Edit any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 465,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Delete", "Delete any event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 466,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.Register", "Mark self as going to an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 467,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Events.ManageAttendees", "Remove attendees from an event" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 468,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.View", "View and download documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 469,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.Upload", "Upload documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 470,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Documents.Delete", "Delete documents" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 537,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Forum.Post.DeleteOwn", "Delete own forum post" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 539,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Wiki", "Wiki.Page.Create", "Create wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 542,
                columns: new[] { "ClaimValue", "Description" },
                values: new object[] { "Wiki.Page.DeleteOwn", "Delete own wiki pages" });

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 568,
                columns: new[] { "Category", "ClaimValue", "Description" },
                values: new object[] { "Documents", "Documents.View", "View and download documents" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 354, "ActivityLogs", "permission", "Reports.View", "View Reports", "00000000-0000-0000-0000-000000000005" },
                    { 355, "ActivityLogs", "permission", "Reports.Create", "Generate specific report", "00000000-0000-0000-0000-000000000005" },
                    { 371, "Analytics", "permission", "Analytics.View", "View analytics and telemetry", "00000000-0000-0000-0000-000000000005" },
                    { 427, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 428, "Forum", "permission", "Forum.Thread.UpdateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000003" },
                    { 433, "Forum", "permission", "Forum.Thread.Pin", "Pin/unpin forum threads", "00000000-0000-0000-0000-000000000003" },
                    { 440, "Wiki", "permission", "Wiki.Page.UpdateOwn", "Update own wiki page", "00000000-0000-0000-0000-000000000003" },
                    { 445, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000003" },
                    { 446, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000003" },
                    { 457, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000003" },
                    { 458, "Announcements", "permission", "Announcements.Create", "Create a new announcements", "00000000-0000-0000-0000-000000000003" },
                    { 463, "Events", "permission", "Events.Create", "Create events", "00000000-0000-0000-0000-000000000003" },
                    { 527, "Forum", "permission", "Forum.Thread.Create", "Create forum threads", "00000000-0000-0000-0000-000000000004" },
                    { 528, "Forum", "permission", "Forum.Thread.UpdateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000004" },
                    { 534, "Forum", "permission", "Forum.Post.Create", "Create posts in threads", "00000000-0000-0000-0000-000000000004" },
                    { 535, "Forum", "permission", "Forum.Post.UpdateOwn", "Update own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 540, "Wiki", "permission", "Wiki.Page.UpdateOwn", "Update own wiki page", "00000000-0000-0000-0000-000000000004" },
                    { 545, "Videos", "permission", "Videos.View", "View video", "00000000-0000-0000-0000-000000000004" },
                    { 546, "Videos", "permission", "Videos.Create", "Upload video", "00000000-0000-0000-0000-000000000004" },
                    { 549, "Videos", "permission", "Videos.DeleteOwn", "Delete own video", "00000000-0000-0000-0000-000000000004" },
                    { 557, "Announcements", "permission", "Announcements.View", "View announcements", "00000000-0000-0000-0000-000000000004" },
                    { 562, "Events", "permission", "Events.View", "View events", "00000000-0000-0000-0000-000000000004" },
                    { 566, "Events", "permission", "Events.Register", "Mark self as going to an event", "00000000-0000-0000-0000-000000000004" }
                });
        }
    }
}
