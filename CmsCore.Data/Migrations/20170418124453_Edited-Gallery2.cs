using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class EditedGallery2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItems_Galleries_GalleryId",
                table: "GalleryItems",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
