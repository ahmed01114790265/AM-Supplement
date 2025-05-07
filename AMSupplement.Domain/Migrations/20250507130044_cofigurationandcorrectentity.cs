using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSupplement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class cofigurationandcorrectentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_Oredrs_OrderId",
                table: "OrederItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_product_ProductId",
                table: "OrederItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Oredrs_Users_UserId",
                table: "Oredrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Oredrs_OrderId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Oredrs",
                table: "Oredrs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "products");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Oredrs",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "OrederItems",
                newName: " OrderItems");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "products",
                newName: "ImageUrl");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_OrderId",
                table: "Payments",
                newName: "IX_Payments_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Oredrs_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_ProductId",
                table: " OrderItems",
                newName: "IX_ OrderItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_OrderId",
                table: " OrderItems",
                newName: "IX_ OrderItems_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Taste",
                table: "products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_products",
                table: "products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ OrderItems",
                table: " OrderItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ OrderItems_Orders_OrderId",
                table: " OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ OrderItems_products_ProductId",
                table: " OrderItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ OrderItems_Orders_OrderId",
                table: " OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ OrderItems_products_ProductId",
                table: " OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products",
                table: "products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ OrderItems",
                table: " OrderItems");

            migrationBuilder.RenameTable(
                name: "products",
                newName: "product");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Oredrs");

            migrationBuilder.RenameTable(
                name: " OrderItems",
                newName: "OrederItems");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "product",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_OrderId",
                table: "Payment",
                newName: "IX_Payment_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Oredrs",
                newName: "IX_Oredrs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ OrderItems_ProductId",
                table: "OrederItems",
                newName: "IX_OrederItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ OrderItems_OrderId",
                table: "OrederItems",
                newName: "IX_OrederItems_OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Taste",
                table: "product",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "product",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                table: "product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Oredrs",
                table: "Oredrs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_Oredrs_OrderId",
                table: "OrederItems",
                column: "OrderId",
                principalTable: "Oredrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_product_ProductId",
                table: "OrederItems",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Oredrs_Users_UserId",
                table: "Oredrs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Oredrs_OrderId",
                table: "Payment",
                column: "OrderId",
                principalTable: "Oredrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
