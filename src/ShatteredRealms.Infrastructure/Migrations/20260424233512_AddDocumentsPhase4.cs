using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShatteredRealms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentsPhase4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalFileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    UploadedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "Category", "ClaimType", "ClaimValue", "Description", "RoleId" },
                values: new object[,]
                {
                    { 168, "Documents", "permission", "Documents.View", "View and download documents", "00000000-0000-0000-0000-000000000001" },
                    { 169, "Documents", "permission", "Documents.Upload", "Upload documents", "00000000-0000-0000-0000-000000000001" },
                    { 170, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000001" },
                    { 268, "Documents", "permission", "Documents.View", "View and download documents", "00000000-0000-0000-0000-000000000002" },
                    { 269, "Documents", "permission", "Documents.Upload", "Upload documents", "00000000-0000-0000-0000-000000000002" },
                    { 270, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000002" },
                    { 468, "Documents", "permission", "Documents.View", "View and download documents", "00000000-0000-0000-0000-000000000003" },
                    { 469, "Documents", "permission", "Documents.Upload", "Upload documents", "00000000-0000-0000-0000-000000000003" },
                    { 470, "Documents", "permission", "Documents.Delete", "Delete documents", "00000000-0000-0000-0000-000000000003" },
                    { 568, "Documents", "permission", "Documents.View", "View and download documents", "00000000-0000-0000-0000-000000000004" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_UploadedById",
                table: "Document",
                column: "UploadedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 568);
        }
    }
}
