using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class sliderChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sliders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TemplateId",
                table: "Sliders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisplayTexts",
                table: "Slides",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Sliders_TemplateId",
                table: "Sliders",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sliders_Templates_TemplateId",
                table: "Sliders",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sliders_Templates_TemplateId",
                table: "Sliders");

            migrationBuilder.DropIndex(
                name: "IX_Sliders_TemplateId",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "DisplayTexts",
                table: "Slides");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sliders",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
