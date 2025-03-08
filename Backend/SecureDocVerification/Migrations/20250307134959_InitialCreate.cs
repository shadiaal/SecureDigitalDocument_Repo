using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SecureDocVerification.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    VerifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationLogs_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Admin User", "admin123", "Admin" },
                    { 2, "user@example.com", "Regular User", "user123", "User" }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "CreatedAt", "FilePath", "Status", "Title", "UserId", "VerificationCode" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 7, 13, 49, 58, 992, DateTimeKind.Utc).AddTicks(6506), "/docs/sample1.pdf", "Pending", "Sample Document 1", 1, "ABC123" },
                    { 2, new DateTime(2025, 3, 7, 13, 49, 58, 992, DateTimeKind.Utc).AddTicks(6509), "/docs/sample2.pdf", "Pending", "Sample Document 2", 2, "XYZ456" }
                });

            migrationBuilder.InsertData(
                table: "VerificationLogs",
                columns: new[] { "Id", "DocumentId", "Status", "Timestamp", "VerifiedBy" },
                values: new object[,]
                {
                    { 1, 1, "Success", new DateTime(2025, 3, 7, 13, 49, 58, 992, DateTimeKind.Utc).AddTicks(6520), "admin@example.com" },
                    { 2, 2, "Failed", new DateTime(2025, 3, 7, 13, 49, 58, 992, DateTimeKind.Utc).AddTicks(6522), "user@example.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationLogs_DocumentId",
                table: "VerificationLogs",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationLogs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
