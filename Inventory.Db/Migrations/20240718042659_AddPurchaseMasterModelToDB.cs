using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseMasterModelToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Items_ItemId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_PurchaseMaster_PurchaseMasterId",
                table: "PurchaseDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseMaster_Vendor_VendorId",
                table: "PurchaseMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseMaster",
                table: "PurchaseMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "Vendors");

            migrationBuilder.RenameTable(
                name: "PurchaseMaster",
                newName: "PurchasesMaster");

            migrationBuilder.RenameTable(
                name: "PurchaseDetail",
                newName: "PurchasesDetail");

            migrationBuilder.RenameColumn(
                name: "contact",
                table: "Vendors",
                newName: "Contact");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseMaster_VendorId",
                table: "PurchasesMaster",
                newName: "IX_PurchasesMaster_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_PurchaseMasterId",
                table: "PurchasesDetail",
                newName: "IX_PurchasesDetail_PurchaseMasterId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseDetail_ItemId",
                table: "PurchasesDetail",
                newName: "IX_PurchasesDetail_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasesMaster",
                table: "PurchasesMaster",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasesDetail",
                table: "PurchasesDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesDetail_Items_ItemId",
                table: "PurchasesDetail",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesDetail_PurchasesMaster_PurchaseMasterId",
                table: "PurchasesDetail",
                column: "PurchaseMasterId",
                principalTable: "PurchasesMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesMaster_Vendors_VendorId",
                table: "PurchasesMaster",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetail_Items_ItemId",
                table: "PurchasesDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetail_PurchasesMaster_PurchaseMasterId",
                table: "PurchasesDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesMaster_Vendors_VendorId",
                table: "PurchasesMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasesMaster",
                table: "PurchasesMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasesDetail",
                table: "PurchasesDetail");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "Vendor");

            migrationBuilder.RenameTable(
                name: "PurchasesMaster",
                newName: "PurchaseMaster");

            migrationBuilder.RenameTable(
                name: "PurchasesDetail",
                newName: "PurchaseDetail");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "Vendor",
                newName: "contact");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesMaster_VendorId",
                table: "PurchaseMaster",
                newName: "IX_PurchaseMaster_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesDetail_PurchaseMasterId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_PurchaseMasterId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesDetail_ItemId",
                table: "PurchaseDetail",
                newName: "IX_PurchaseDetail_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseMaster",
                table: "PurchaseMaster",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseDetail",
                table: "PurchaseDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Items_ItemId",
                table: "PurchaseDetail",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_PurchaseMaster_PurchaseMasterId",
                table: "PurchaseDetail",
                column: "PurchaseMasterId",
                principalTable: "PurchaseMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseMaster_Vendor_VendorId",
                table: "PurchaseMaster",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
