using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeyListen.Migrations
{
    public partial class AddKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentDjID",
                table: "Channels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_CurrentDjID",
                table: "Channels",
                column: "CurrentDjID");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Users_CurrentDjID",
                table: "Channels",
                column: "CurrentDjID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Users_CurrentDjID",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_CurrentDjID",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "CurrentDjID",
                table: "Channels");
        }
    }
}
