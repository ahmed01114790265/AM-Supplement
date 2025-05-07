using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMSupplement.Domain.Migrations
{
    /// <inheritdoc />
    public partial class configureentites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_Oredrs_OredeId",
                table: "OrederItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Oredrs_OrdeId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PaymentProcess_PaymentprocessId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentProcess");

            migrationBuilder.DropIndex(
                name: "IX_Payment_OrdeId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Amountorders",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "OrdeId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Pricepurchase",
                table: "OrederItems");

            migrationBuilder.RenameColumn(
                name: "PaymentprocessId",
                table: "Payment",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PaymentprocessId",
                table: "Payment",
                newName: "IX_Payment_OrderId");

            migrationBuilder.RenameColumn(
                name: "OredeId",
                table: "OrederItems",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_OredeId",
                table: "OrederItems",
                newName: "IX_OrederItems_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "product",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "Paymentstaus",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_Email_Format",
                table: "Users",
                sql: "[Email] LIKE '%@%.%' AND CHARINDEX(' ', [Email]) = 0");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_Oredrs_OrderId",
                table: "OrederItems",
                column: "OrderId",
                principalTable: "Oredrs",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_Oredrs_OrderId",
                table: "OrederItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Oredrs_OrderId",
                table: "Payment");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_Email_Format",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "product");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "product");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Payment",
                newName: "PaymentprocessId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_OrderId",
                table: "Payment",
                newName: "IX_Payment_PaymentprocessId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrederItems",
                newName: "OredeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_OrderId",
                table: "OrederItems",
                newName: "IX_OrederItems_OredeId");

            migrationBuilder.AlterColumn<string>(
                name: "Paymentstaus",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Amountorders",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrdeId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Pricepurchase",
                table: "OrederItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "PaymentProcess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProcess", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrdeId",
                table: "Payment",
                column: "OrdeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_Oredrs_OredeId",
                table: "OrederItems",
                column: "OredeId",
                principalTable: "Oredrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Oredrs_OrdeId",
                table: "Payment",
                column: "OrdeId",
                principalTable: "Oredrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentProcess_PaymentprocessId",
                table: "Payment",
                column: "PaymentprocessId",
                principalTable: "PaymentProcess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
