using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsCore.Data.Migrations
{
    public partial class EditedGalleryItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItemCategories_GalleryItems_GalleryItemId",
                table: "GalleryItemCategories");

            migrationBuilder.DropIndex(
                name: "IX_GalleryItemCategories_GalleryItemId",
                table: "GalleryItemCategories");

            migrationBuilder.DropColumn(
                name: "GalleryItemId",
                table: "GalleryItemCategories");

            migrationBuilder.AddColumn<string>(
                name: "Meta1",
                table: "GalleryItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meta1",
                table: "GalleryItems");

            migrationBuilder.AddColumn<long>(
                name: "GalleryItemId",
                table: "GalleryItemCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GalleryItemCategories_GalleryItemId",
                table: "GalleryItemCategories",
                column: "GalleryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItemCategories_GalleryItems_GalleryItemId",
                table: "GalleryItemCategories",
                column: "GalleryItemId",
                principalTable: "GalleryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
