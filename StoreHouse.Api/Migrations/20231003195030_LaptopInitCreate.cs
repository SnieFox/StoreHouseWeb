using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class LaptopInitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_DishesCategories_CategoryId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientsCategories_CategoryId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Dishes_DishId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Receipts_ReceiptId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Supplies_SupplyId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_WriteOffs_WriteOffId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Clients_ClientId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Users_UserId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplies_Users_UserId",
                table: "Supplies");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WriteOffs_Users_UserId",
                table: "WriteOffs");

            migrationBuilder.DropForeignKey(
                name: "FK_WriteOffs_WriteOffCauses_CauseId",
                table: "WriteOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WriteOffs",
                table: "WriteOffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplies",
                table: "Supplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes");

            migrationBuilder.RenameTable(
                name: "WriteOffs",
                newName: "WriteOff");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Supplies",
                newName: "Supply");

            migrationBuilder.RenameTable(
                name: "Receipts",
                newName: "Receipt");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Dishes",
                newName: "Dish");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOffs_UserId",
                table: "WriteOff",
                newName: "IX_WriteOff_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOffs_CauseId",
                table: "WriteOff",
                newName: "IX_WriteOff_CauseId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "User",
                newName: "IX_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplies_UserId",
                table: "Supply",
                newName: "IX_Supply_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Supplies_SupplierId",
                table: "Supply",
                newName: "IX_Supply_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_UserId",
                table: "Receipt",
                newName: "IX_Receipt_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_ClientId",
                table: "Receipt",
                newName: "IX_Receipt_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_CategoryId",
                table: "Ingredient",
                newName: "IX_Ingredient_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Dishes_CategoryId",
                table: "Dish",
                newName: "IX_Dish_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WriteOff",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CauseId",
                table: "WriteOff",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Supply",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Supply",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Receipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Receipt",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WriteOff",
                table: "WriteOff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supply",
                table: "Supply",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dish",
                table: "Dish",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_DishesCategories_CategoryId",
                table: "Dish",
                column: "CategoryId",
                principalTable: "DishesCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_IngredientsCategories_CategoryId",
                table: "Ingredient",
                column: "CategoryId",
                principalTable: "IngredientsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategories_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Dish_DishId",
                table: "ProductList",
                column: "DishId",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Receipt_ReceiptId",
                table: "ProductList",
                column: "ReceiptId",
                principalTable: "Receipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Supply_SupplyId",
                table: "ProductList",
                column: "SupplyId",
                principalTable: "Supply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_WriteOff_WriteOffId",
                table: "ProductList",
                column: "WriteOffId",
                principalTable: "WriteOff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Clients_ClientId",
                table: "Receipt",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_User_UserId",
                table: "Receipt",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_Suppliers_SupplierId",
                table: "Supply",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_User_UserId",
                table: "Supply",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOff_User_UserId",
                table: "WriteOff",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOff_WriteOffCauses_CauseId",
                table: "WriteOff",
                column: "CauseId",
                principalTable: "WriteOffCauses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_DishesCategories_CategoryId",
                table: "Dish");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_IngredientsCategories_CategoryId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategories_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Dish_DishId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Receipt_ReceiptId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_Supply_SupplyId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductList_WriteOff_WriteOffId",
                table: "ProductList");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_Clients_ClientId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_User_UserId",
                table: "Receipt");

            migrationBuilder.DropForeignKey(
                name: "FK_Supply_Suppliers_SupplierId",
                table: "Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_Supply_User_UserId",
                table: "Supply");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_RoleId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_WriteOff_User_UserId",
                table: "WriteOff");

            migrationBuilder.DropForeignKey(
                name: "FK_WriteOff_WriteOffCauses_CauseId",
                table: "WriteOff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WriteOff",
                table: "WriteOff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supply",
                table: "Supply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Receipt",
                table: "Receipt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dish",
                table: "Dish");

            migrationBuilder.RenameTable(
                name: "WriteOff",
                newName: "WriteOffs");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Supply",
                newName: "Supplies");

            migrationBuilder.RenameTable(
                name: "Receipt",
                newName: "Receipts");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Dish",
                newName: "Dishes");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOff_UserId",
                table: "WriteOffs",
                newName: "IX_WriteOffs_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WriteOff_CauseId",
                table: "WriteOffs",
                newName: "IX_WriteOffs_CauseId");

            migrationBuilder.RenameIndex(
                name: "IX_User_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Supply_UserId",
                table: "Supplies",
                newName: "IX_Supplies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Supply_SupplierId",
                table: "Supplies",
                newName: "IX_Supplies_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_UserId",
                table: "Receipts",
                newName: "IX_Receipts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipt_ClientId",
                table: "Receipts",
                newName: "IX_Receipts_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_CategoryId",
                table: "Ingredients",
                newName: "IX_Ingredients_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Dish_CategoryId",
                table: "Dishes",
                newName: "IX_Dishes_CategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "WriteOffs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CauseId",
                table: "WriteOffs",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Supplies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Supplies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WriteOffs",
                table: "WriteOffs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplies",
                table: "Supplies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Receipts",
                table: "Receipts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_DishesCategories_CategoryId",
                table: "Dishes",
                column: "CategoryId",
                principalTable: "DishesCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientsCategories_CategoryId",
                table: "Ingredients",
                column: "CategoryId",
                principalTable: "IngredientsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Dishes_DishId",
                table: "ProductList",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Receipts_ReceiptId",
                table: "ProductList",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_Supplies_SupplyId",
                table: "ProductList",
                column: "SupplyId",
                principalTable: "Supplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductList_WriteOffs_WriteOffId",
                table: "ProductList",
                column: "WriteOffId",
                principalTable: "WriteOffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Clients_ClientId",
                table: "Receipts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Users_UserId",
                table: "Receipts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Suppliers_SupplierId",
                table: "Supplies",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supplies_Users_UserId",
                table: "Supplies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOffs_Users_UserId",
                table: "WriteOffs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WriteOffs_WriteOffCauses_CauseId",
                table: "WriteOffs",
                column: "CauseId",
                principalTable: "WriteOffCauses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
