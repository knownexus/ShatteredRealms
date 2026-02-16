using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WikiCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumCategory_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WikiPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CurrentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WikiPage_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserEmergencyContact",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmergencyContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmergencyContact", x => new { x.UserId, x.EmergencyContactId });
                    table.ForeignKey(
                        name: "FK_UserEmergencyContact_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEmergencyContact_EmergencyContact_EmergencyContactId",
                        column: x => x.EmergencyContactId,
                        principalTable: "EmergencyContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumThread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumThread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumThread_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForumThread_ForumCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ForumCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WikiPageCategory",
                columns: table => new
                {
                    WikiPageId = table.Column<int>(type: "int", nullable: false),
                    WikiCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiPageCategory", x => new { x.WikiPageId, x.WikiCategoryId });
                    table.ForeignKey(
                        name: "FK_WikiPageCategory_WikiCategory_WikiCategoryId",
                        column: x => x.WikiCategoryId,
                        principalTable: "WikiCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WikiPageCategory_WikiPage_WikiPageId",
                        column: x => x.WikiPageId,
                        principalTable: "WikiPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WikiRevision",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WikiPageId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RevisionNote = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WikiRevision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WikiRevision_AspNetUsers_EditorId",
                        column: x => x.EditorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WikiRevision_WikiPage_WikiPageId",
                        column: x => x.WikiPageId,
                        principalTable: "WikiPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForumPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumPost_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ForumPost_ForumThread_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "ForumThread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "IsSystem", "Name", "NormalizedName", "Priority" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", null, "Internal system role, unrestricted", true, "System", "SYSTEM", 100 },
                    { "00000000-0000-0000-0000-000000000002", null, "Full administrative access", true, "Admin", "ADMIN", 90 },
                    { "00000000-0000-0000-0000-000000000003", null, "Can manage characters, assign positions/experience", false, "EventOrganizer", "EVENTORGANIZER", 50 },
                    { "00000000-0000-0000-0000-000000000004", null, "Standard registered user", false, "User", "USER", 10 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 101, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000001" },
                    { 102, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000001" },
                    { 103, "Users", "permission", "Users.View", "View any user", "00000000-0000-0000-0000-000000000001" },
                    { 104, "Users", "permission", "Users.Create", "Create users", "00000000-0000-0000-0000-000000000001" },
                    { 105, "Users", "permission", "Users.Update", "Update any user", "00000000-0000-0000-0000-000000000001" },
                    { 106, "Users", "permission", "Users.Delete", "Delete users", "00000000-0000-0000-0000-000000000001" },
                    { 107, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000001" },
                    { 108, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000001" },
                    { 109, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000001" },
                    { 110, "Characters", "permission", "Characters.View", "View any character", "00000000-0000-0000-0000-000000000001" },
                    { 111, "Characters", "permission", "Characters.Update", "Update any character", "00000000-0000-0000-0000-000000000001" },
                    { 112, "Characters", "permission", "Characters.Delete", "Delete any character", "00000000-0000-0000-0000-000000000001" },
                    { 113, "Characters", "permission", "Characters.AssignPosition", "Assign societal/military position", "00000000-0000-0000-0000-000000000001" },
                    { 114, "Characters", "permission", "Characters.AssignExperience", "Assign experience/level", "00000000-0000-0000-0000-000000000001" },
                    { 115, "Role", "permission", "Role.View", "View roles", "00000000-0000-0000-0000-000000000001" },
                    { 116, "Role", "permission", "Role.Create", "Create roles", "00000000-0000-0000-0000-000000000001" },
                    { 117, "Role", "permission", "Role.Update", "Update roles", "00000000-0000-0000-0000-000000000001" },
                    { 118, "Role", "permission", "Role.Delete", "Delete roles", "00000000-0000-0000-0000-000000000001" },
                    { 119, "Role", "permission", "Role.Assign", "Assign roles to users", "00000000-0000-0000-0000-000000000001" },
                    { 120, "PermissionControl", "permission", "PermissionControl.View", "View permissions", "00000000-0000-0000-0000-000000000001" },
                    { 121, "PermissionControl", "permission", "PermissionControl.Assign", "Assign permissions to roles", "00000000-0000-0000-0000-000000000001" },
                    { 122, "Forum", "permission", "forum.category.create", "Create forum categories", "00000000-0000-0000-0000-000000000001" },
                    { 123, "Forum", "permission", "forum.category.edit", "Update forum categories", "00000000-0000-0000-0000-000000000001" },
                    { 124, "Forum", "permission", "forum.category.delete", "Delete forum categories", "00000000-0000-0000-0000-000000000001" },
                    { 125, "Forum", "permission", "forum.thread.create", "Create forum threads", "00000000-0000-0000-0000-000000000001" },
                    { 126, "Forum", "permission", "forum.thread.update", "Update any forum thread", "00000000-0000-0000-0000-000000000001" },
                    { 127, "Forum", "permission", "forum.thread.delete", "Delete any forum thread", "00000000-0000-0000-0000-000000000001" },
                    { 128, "Forum", "permission", "forum.thread.lock", "Lock/unlock forum threads", "00000000-0000-0000-0000-000000000001" },
                    { 129, "Forum", "permission", "forum.thread.pin", "Pin/unpin forum threads", "00000000-0000-0000-0000-000000000001" },
                    { 130, "Forum", "permission", "forum.post.create", "Create posts in threads", "00000000-0000-0000-0000-000000000001" },
                    { 131, "Forum", "permission", "forum.post.edit", "Update any forum post", "00000000-0000-0000-0000-000000000001" },
                    { 132, "Forum", "permission", "forum.post.delete", "Delete any forum post", "00000000-0000-0000-0000-000000000001" },
                    { 133, "Forum", "permission", "forum.thread.updateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000001" },
                    { 134, "Forum", "permission", "forum.thread.deleteOwn", "Delete own forum thread", "00000000-0000-0000-0000-000000000001" },
                    { 135, "Forum", "permission", "forum.post.editOwn", "Update own forum post", "00000000-0000-0000-0000-000000000001" },
                    { 136, "Forum", "permission", "forum.post.deleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000001" },
                    { 137, "Wiki", "permission", "wiki.page.create", "Create wiki pages", "00000000-0000-0000-0000-000000000001" },
                    { 138, "Wiki", "permission", "wiki.page.edit", "Update any wiki page", "00000000-0000-0000-0000-000000000001" },
                    { 139, "Wiki", "permission", "wiki.page.editOwn", "Update any wiki page", "00000000-0000-0000-0000-000000000001" },
                    { 140, "Wiki", "permission", "wiki.page.delete", "Delete wiki pages", "00000000-0000-0000-0000-000000000001" },
                    { 141, "Wiki", "permission", "wiki.page.deleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000001" },
                    { 142, "Wiki", "permission", "wiki.category.manage", "Create/update wiki categories", "00000000-0000-0000-0000-000000000001" },
                    { 201, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000002" },
                    { 202, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000002" },
                    { 203, "Users", "permission", "Users.View", "View any user", "00000000-0000-0000-0000-000000000002" },
                    { 204, "Users", "permission", "Users.Create", "Create users", "00000000-0000-0000-0000-000000000002" },
                    { 205, "Users", "permission", "Users.Update", "Update any user", "00000000-0000-0000-0000-000000000002" },
                    { 206, "Users", "permission", "Users.Delete", "Delete users", "00000000-0000-0000-0000-000000000002" },
                    { 207, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000002" },
                    { 208, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000002" },
                    { 209, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000002" },
                    { 210, "Characters", "permission", "Characters.View", "View any character", "00000000-0000-0000-0000-000000000002" },
                    { 211, "Characters", "permission", "Characters.Update", "Update any character", "00000000-0000-0000-0000-000000000002" },
                    { 212, "Characters", "permission", "Characters.Delete", "Delete any character", "00000000-0000-0000-0000-000000000002" },
                    { 213, "Characters", "permission", "Characters.AssignPosition", "Assign societal/military position", "00000000-0000-0000-0000-000000000002" },
                    { 214, "Characters", "permission", "Characters.AssignExperience", "Assign experience/level", "00000000-0000-0000-0000-000000000002" },
                    { 215, "Role", "permission", "Role.View", "View roles", "00000000-0000-0000-0000-000000000002" },
                    { 216, "Role", "permission", "Role.Create", "Create roles", "00000000-0000-0000-0000-000000000002" },
                    { 217, "Role", "permission", "Role.Update", "Update roles", "00000000-0000-0000-0000-000000000002" },
                    { 218, "Role", "permission", "Role.Delete", "Delete roles", "00000000-0000-0000-0000-000000000002" },
                    { 219, "Role", "permission", "Role.Assign", "Assign roles to users", "00000000-0000-0000-0000-000000000002" },
                    { 220, "PermissionControl", "permission", "PermissionControl.View", "View permissions", "00000000-0000-0000-0000-000000000002" },
                    { 221, "PermissionControl", "permission", "PermissionControl.Assign", "Assign permissions to roles", "00000000-0000-0000-0000-000000000002" },
                    { 222, "Forum", "permission", "forum.category.create", "Create forum categories", "00000000-0000-0000-0000-000000000002" },
                    { 223, "Forum", "permission", "forum.category.edit", "Update forum categories", "00000000-0000-0000-0000-000000000002" },
                    { 224, "Forum", "permission", "forum.category.delete", "Delete forum categories", "00000000-0000-0000-0000-000000000002" },
                    { 225, "Forum", "permission", "forum.thread.create", "Create forum threads", "00000000-0000-0000-0000-000000000002" },
                    { 226, "Forum", "permission", "forum.thread.update", "Update any forum thread", "00000000-0000-0000-0000-000000000002" },
                    { 227, "Forum", "permission", "forum.thread.delete", "Delete any forum thread", "00000000-0000-0000-0000-000000000002" },
                    { 228, "Forum", "permission", "forum.thread.lock", "Lock/unlock forum threads", "00000000-0000-0000-0000-000000000002" },
                    { 229, "Forum", "permission", "forum.thread.pin", "Pin/unpin forum threads", "00000000-0000-0000-0000-000000000002" },
                    { 230, "Forum", "permission", "forum.post.create", "Create posts in threads", "00000000-0000-0000-0000-000000000002" },
                    { 231, "Forum", "permission", "forum.post.edit", "Update any forum post", "00000000-0000-0000-0000-000000000002" },
                    { 232, "Forum", "permission", "forum.post.delete", "Delete any forum post", "00000000-0000-0000-0000-000000000002" },
                    { 233, "Forum", "permission", "forum.thread.updateOwn", "Update own forum thread", "00000000-0000-0000-0000-000000000002" },
                    { 234, "Forum", "permission", "forum.thread.deleteOwn", "Delete own forum thread", "00000000-0000-0000-0000-000000000002" },
                    { 235, "Forum", "permission", "forum.post.editOwn", "Update own forum post", "00000000-0000-0000-0000-000000000002" },
                    { 236, "Forum", "permission", "forum.post.deleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000002" },
                    { 237, "Wiki", "permission", "wiki.page.create", "Create wiki pages", "00000000-0000-0000-0000-000000000002" },
                    { 238, "Wiki", "permission", "wiki.page.edit", "Update any wiki page", "00000000-0000-0000-0000-000000000002" },
                    { 239, "Wiki", "permission", "wiki.page.editOwn", "Update any wiki page", "00000000-0000-0000-0000-000000000002" },
                    { 240, "Wiki", "permission", "wiki.page.delete", "Delete wiki pages", "00000000-0000-0000-0000-000000000002" },
                    { 241, "Wiki", "permission", "wiki.page.deleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000002" },
                    { 242, "Wiki", "permission", "wiki.category.manage", "Create/update wiki categories", "00000000-0000-0000-0000-000000000002" },
                    { 301, "Users", "permission", "Users.ViewOwn", "View own profile", "00000000-0000-0000-0000-000000000003" },
                    { 302, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000003" },
                    { 307, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000003" },
                    { 308, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000003" },
                    { 309, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000003" },
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
                    { 402, "Users", "permission", "Users.UpdateOwn", "Update own profile", "00000000-0000-0000-0000-000000000004" },
                    { 407, "Characters", "permission", "Characters.ViewOwn", "View own characters", "00000000-0000-0000-0000-000000000004" },
                    { 408, "Characters", "permission", "Characters.CreateOwn", "Create own characters", "00000000-0000-0000-0000-000000000004" },
                    { 409, "Characters", "permission", "Characters.UpdateOwn", "Update own character name/nation", "00000000-0000-0000-0000-000000000004" },
                    { 430, "Forum", "permission", "forum.post.create", "Create posts in threads", "00000000-0000-0000-0000-000000000004" },
                    { 435, "Forum", "permission", "forum.post.editOwn", "Update own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 436, "Forum", "permission", "forum.post.deleteOwn", "Delete own forum post", "00000000-0000-0000-0000-000000000004" },
                    { 437, "Wiki", "permission", "wiki.page.create", "Create wiki pages", "00000000-0000-0000-0000-000000000004" },
                    { 439, "Wiki", "permission", "wiki.page.editOwn", "Update any wiki page", "00000000-0000-0000-0000-000000000004" },
                    { 441, "Wiki", "permission", "wiki.page.deleteOwn", "Delete own wiki pages", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ForumCategory_CreatedById",
                table: "ForumCategory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPost_AuthorId",
                table: "ForumPost",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPost_ThreadId",
                table: "ForumPost",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThread_AuthorId",
                table: "ForumThread",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumThread_CategoryId",
                table: "ForumThread",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmergencyContact_EmergencyContactId",
                table: "UserEmergencyContact",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_WikiPage_AuthorId",
                table: "WikiPage",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WikiPage_Slug",
                table: "WikiPage",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WikiPageCategory_WikiCategoryId",
                table: "WikiPageCategory",
                column: "WikiCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WikiRevision_EditorId",
                table: "WikiRevision",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_WikiRevision_WikiPageId",
                table: "WikiRevision",
                column: "WikiPageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ForumPost");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UserEmergencyContact");

            migrationBuilder.DropTable(
                name: "WikiPageCategory");

            migrationBuilder.DropTable(
                name: "WikiRevision");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "ForumThread");

            migrationBuilder.DropTable(
                name: "EmergencyContact");

            migrationBuilder.DropTable(
                name: "WikiCategory");

            migrationBuilder.DropTable(
                name: "WikiPage");

            migrationBuilder.DropTable(
                name: "ForumCategory");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
