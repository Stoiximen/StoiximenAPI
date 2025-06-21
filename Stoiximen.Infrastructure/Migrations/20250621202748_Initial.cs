using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stoiximen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubscriptionStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Name", "Price", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM", null, null, "Basic access to the platform.", "Basic Plan", 9.99m, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM" },
                    { 2, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM", null, null, "Full access to all features.", "Premium Plan", 29.99m, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Name", "SubscriptionStatus", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { "1111 Vale to telegramId sou edw", new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM", null, null, "Tony rs", 0, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM" },
                    { "2222 Vale to telegramId sou edw", new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM", null, null, "Xaris tsel", 0, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM" },
                    { "8011353457", new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM", null, null, "GN", 0, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Utc), "SYSTEM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
