using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Migrations
{
    /// <inheritdoc />
    public partial class AmazonDBModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3abdce32-3740-4311-8f9c-7e62e8f45924");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9");

            migrationBuilder.AlterColumn<string>(
                name: "Card_Num",
                table: "Payment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af47cffd-60e9-442f-ad06-f8839359bce6", null, "Admin", "ADMIN" },
                    { "e9c645a0-38b1-47cc-8554-298fb3ff2a8b", null, "Client", "CLIENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c8b6b1-e2b6-45e8-b044-8f2178a90111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0095c986-0e7a-4b68-8607-a85b050f0fdc", "AQAAAAIAAYagAAAAEF1yYc9+hQbqUPIegsonh77yAgEfPw+RZDxlTeT6eZxdAjzLLQqh+TFOzMcKvquIcg==", "ec695da3-c13e-4c86-8236-1c370e457b9f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "af47cffd-60e9-442f-ad06-f8839359bce6", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c645a0-38b1-47cc-8554-298fb3ff2a8b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af47cffd-60e9-442f-ad06-f8839359bce6", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af47cffd-60e9-442f-ad06-f8839359bce6");

            migrationBuilder.AlterColumn<int>(
                name: "Card_Num",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3abdce32-3740-4311-8f9c-7e62e8f45924", null, "Client", "CLIENT" },
                    { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c8b6b1-e2b6-45e8-b044-8f2178a90111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0eea46d1-55b9-4d2b-b708-a3ce22907ee3", "AQAAAAIAAYagAAAAEHCgFkSNOD37P/B5bdUWsaM5P83Q9jwWWBuF9wdUziyMQA4hE78wyY5cMb8XyjEqwg==", "90627934-d8f5-49e5-8e22-9abf24cc81d5" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "52342cdd-1cc8-4ae8-8e41-a4b78ed0a5a9", "80c8b6b1-e2b6-45e8-b044-8f2178a90111" });
        }
    }
}
