﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace JobTrack.Data.Migrations
{
    public partial class Add_Appuser_Jobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "JobUserId",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Job",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Job",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "JobUserId",
                table: "Job",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_ApplicationUserId",
                table: "Job",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
