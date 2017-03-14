using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class AddedSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slides_Slider_SliderId",
                table: "Slides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slider",
                table: "Slider");

            migrationBuilder.RenameTable(
                name: "Slider",
                newName: "Sliders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slides_Sliders_SliderId",
                table: "Slides",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slides_Sliders_SliderId",
                table: "Slides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sliders",
                table: "Sliders");

            migrationBuilder.RenameTable(
                name: "Sliders",
                newName: "Slider");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slider",
                table: "Slider",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slides_Slider_SliderId",
                table: "Slides",
                column: "SliderId",
                principalTable: "Slider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
