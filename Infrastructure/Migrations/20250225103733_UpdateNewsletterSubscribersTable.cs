using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewsletterSubscribersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "NewsletterSubscribers",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "NewsletterSubscribers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterSubscribers_Email",
                table: "NewsletterSubscribers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NewsletterSubscribers_Email",
                table: "NewsletterSubscribers");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "NewsletterSubscribers",
                newName: "IsConfirmed");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "NewsletterSubscribers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
