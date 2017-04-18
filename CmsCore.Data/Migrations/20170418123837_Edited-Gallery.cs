using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class EditedGallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems");

            migrationBuilder.AlterColumn<long>(
                name: "GalleryId",
                table: "GalleryItems",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems");

            migrationBuilder.AlterColumn<long>(
                name: "GalleryId",
                table: "GalleryItems",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
