using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeTrainingSystem.Migrations
{
    public partial class initteach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Coursewares",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Coursewares",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Coursewares",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Coursewares",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Coursewares");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Coursewares");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Coursewares");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Coursewares");
        }
    }
}
