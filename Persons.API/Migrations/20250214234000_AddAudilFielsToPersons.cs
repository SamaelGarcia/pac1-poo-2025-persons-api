using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAudilFielsToPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_by",
                table: "persons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "created_date",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "update_by",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_date",
                table: "persons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "update_by",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "update_date",
                table: "persons");
        }
    }
}
