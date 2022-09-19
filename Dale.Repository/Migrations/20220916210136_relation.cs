using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dale.Repository.Migrations
{
    public partial class relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "CLIENTS");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ITEMSORDER",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ITEMSORDER_ProductId",
                table: "ITEMSORDER",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ITEMSORDER_PRODUCTS_ProductId",
                table: "ITEMSORDER",
                column: "ProductId",
                principalTable: "PRODUCTS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ITEMSORDER_PRODUCTS_ProductId",
                table: "ITEMSORDER");

            migrationBuilder.DropIndex(
                name: "IX_ITEMSORDER_ProductId",
                table: "ITEMSORDER");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ITEMSORDER");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "CLIENTS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
