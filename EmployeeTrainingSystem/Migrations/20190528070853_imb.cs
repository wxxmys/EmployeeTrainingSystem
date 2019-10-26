using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeTrainingSystem.Migrations
{
    public partial class imb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "TrainResource");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "TrainResource",
                newName: "Resourcecontent");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassScheduleNameID",
                table: "TrainResource",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Person",
                table: "TrainResource",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeachingDirectionId",
                table: "TrainResource",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainResource_ClassScheduleNameID",
                table: "TrainResource",
                column: "ClassScheduleNameID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainResource_TeachingDirectionId",
                table: "TrainResource",
                column: "TeachingDirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainResource_ClassSchedule_ClassScheduleNameID",
                table: "TrainResource",
                column: "ClassScheduleNameID",
                principalTable: "ClassSchedule",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainResource_Member_TeachingDirectionId",
                table: "TrainResource",
                column: "TeachingDirectionId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainResource_ClassSchedule_ClassScheduleNameID",
                table: "TrainResource");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainResource_Member_TeachingDirectionId",
                table: "TrainResource");

            migrationBuilder.DropIndex(
                name: "IX_TrainResource_ClassScheduleNameID",
                table: "TrainResource");

            migrationBuilder.DropIndex(
                name: "IX_TrainResource_TeachingDirectionId",
                table: "TrainResource");

            migrationBuilder.DropColumn(
                name: "ClassScheduleNameID",
                table: "TrainResource");

            migrationBuilder.DropColumn(
                name: "Person",
                table: "TrainResource");

            migrationBuilder.DropColumn(
                name: "TeachingDirectionId",
                table: "TrainResource");

            migrationBuilder.RenameColumn(
                name: "Resourcecontent",
                table: "TrainResource",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "TrainResource",
                nullable: false,
                defaultValue: "");
        }
    }
}
