using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "1", "Admin", "ADMIN" },
                    { "b332b209-871f-45fc-9a8d-f357f9bff3b1", "2", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ChildrenCount", "ConcurrencyStamp", "DateCreated", "Email", "EmailConfirmed", "FirstName", "Gender", "HasChildren", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MaritalStatus", "NormalizedEmail", "NormalizedUserName", "Online", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7e53a491-a9de-4c75-af44-ff3271a5176c", 0, 0, "5dfd06ee-0e7c-44a9-92c3-0c63b441db7c", new DateTime(2025, 2, 2, 17, 3, 36, 632, DateTimeKind.Utc).AddTicks(2364), "super@admin.com", true, "Super", 2, false, false, "Admin", false, null, 1, "SUPER@ADMIN.COM", "SUPER_ADMIN", false, "AQAAAAIAAYagAAAAEJXOu25DcpLA+fM1mNlcK8uZ+EtDDegqzoPWljdhfajBz9+5biwYdcTs5dMqe1tzyA==", "01234567891", true, "069f7db7-876c-40ba-bacd-f2bd82514e93", false, "super_admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "7e53a491-a9de-4c75-af44-ff3271a5176c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b332b209-871f-45fc-9a8d-f357f9bff3b1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a330b209-871f-45fc-9a8d-f357f9bff3b1", "7e53a491-a9de-4c75-af44-ff3271a5176c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a330b209-871f-45fc-9a8d-f357f9bff3b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e53a491-a9de-4c75-af44-ff3271a5176c");
        }
    }
}
