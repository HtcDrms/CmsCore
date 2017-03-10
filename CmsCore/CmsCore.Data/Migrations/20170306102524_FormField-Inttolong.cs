using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class FormFieldInttolong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Languages_LanguageId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_Forms_FormId1",
                table: "FormFields");

            migrationBuilder.DropIndex(
                name: "IX_FormFields_FormId1",
                table: "FormFields");

            migrationBuilder.DropColumn(
                name: "FormId1",
                table: "FormFields");

            migrationBuilder.AlterColumn<long>(
                name: "FormId",
                table: "FormFields",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_FormId",
                table: "FormFields",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Languages_LanguageId",
                table: "Forms",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_Forms_FormId",
                table: "FormFields",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Languages_LanguageId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_Forms_FormId",
                table: "FormFields");

            migrationBuilder.DropIndex(
                name: "IX_FormFields_FormId",
                table: "FormFields");

            migrationBuilder.AlterColumn<int>(
                name: "FormId",
                table: "FormFields",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FormId1",
                table: "FormFields",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormFields_FormId1",
                table: "FormFields",
                column: "FormId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Languages_LanguageId",
                table: "Forms",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_Forms_FormId1",
                table: "FormFields",
                column: "FormId1",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
