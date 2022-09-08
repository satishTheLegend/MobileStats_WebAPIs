using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileStats_WebAPIs.Migrations
{
    /// <inheritdoc />
    public partial class AddedDBAndTablesWithRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remove_dt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Added_dt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_dt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Device_id = table.Column<int>(type: "int", nullable: false),
                    Url_hash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Released_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Os = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Storage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Display_size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Display_resolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Camera_pixels = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video_pixels = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chipset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Battery_size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Battery_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Dis_Price = table.Column<int>(type: "int", nullable: true),
                    Max_discount = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileSellingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Transaction_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Device_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMEI_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Original_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxDiscount_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Selling_price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Max_discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Given_discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDiscountApplied = table.Column<bool>(type: "bit", nullable: true),
                    Selling_dt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionUpdated_at = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buyer_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileSellingRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileBrands");

            migrationBuilder.DropTable(
                name: "MobileDevices");

            migrationBuilder.DropTable(
                name: "MobileSellingRecords");
        }
    }
}
