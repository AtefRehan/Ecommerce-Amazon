using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AmazonDBModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_ProductOrdersOrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_OrderProductsProductId",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductOrdersOrderId",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "ProductOrdersOrderId",
                table: "OrderProduct",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "OrderProductsProductId",
                table: "OrderProduct",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3abdce32-3740-4311-8f9c-7e62e8f45924", null, "Client", "CLIENT" },
                    { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "80c8b6b1-e2b6-45e8-b044-8f2178a90111", 0, 1, "0eea46d1-55b9-4d2b-b708-a3ce22907ee3", "admin@amazon.com", false, false, null, "ADMIN@AMAZON.COM", "ADMIN@AMAZON.COM", "AQAAAAIAAYagAAAAEHCgFkSNOD37P/B5bdUWsaM5P83Q9jwWWBuF9wdUziyMQA4hE78wyY5cMb8XyjEqwg==", null, false, "90627934-d8f5-49e5-8e22-9abf24cc81d5", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "ApplicationUserId" },
                values: new object[] { 1, "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId",
                table: "OrderProduct",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3abdce32-3740-4311-8f9c-7e62e8f45924");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });

            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c8b6b1-e2b6-45e8-b044-8f2178a90111");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderProduct");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderProduct",
                newName: "ProductOrdersOrderId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProduct",
                newName: "OrderProductsProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderProductsProductId", "ProductOrdersOrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductOrdersOrderId",
                table: "OrderProduct",
                column: "ProductOrdersOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_ProductOrdersOrderId",
                table: "OrderProduct",
                column: "ProductOrdersOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_OrderProductsProductId",
                table: "OrderProduct",
                column: "OrderProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
