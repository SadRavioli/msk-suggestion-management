using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSKSuggestionManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Suggestions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Suggestions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Suggestions");
        }
    }
}
