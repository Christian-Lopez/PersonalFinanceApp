using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalFinanceApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeCategoriesGlobal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tags",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "categories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "6bd3f737-d8ed-448d-ac33-54e3a2db486e");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "ColorHex", "CreatedAtUtc", "Icon", "Name", "ParentCategoryId", "UpdatedAtUtc", "UserId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Housing", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Food & Dining", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Transportation", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Utilities", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "#059669", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Income", null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tags",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "categories",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "f72408fd-6006-4cd8-8ce3-e34c42c6a3f9");
        }
    }
}
