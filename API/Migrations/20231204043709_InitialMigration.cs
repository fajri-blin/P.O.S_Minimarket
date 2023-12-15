using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_product",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    barcode_id = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_product", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_unit",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_unit", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_role_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_price",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_price", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_product_product_guid",
                        column: x => x.product_guid,
                        principalTable: "tb_m_product",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_tr_price_tb_m_unit_price_guid",
                        column: x => x.price_guid,
                        principalTable: "tb_m_unit",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_transaction",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    employee_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_transaction", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_tr_transaction_tb_m_employee_employee_guid",
                        column: x => x.employee_guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateTable(
                name: "tb_m_transaction_item",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    quantity = table.Column<float>(type: "real", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_transaction_item", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_m_product_product_guid",
                        column: x => x.product_guid,
                        principalTable: "tb_m_product",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_price_transaction_guid",
                        column: x => x.transaction_guid,
                        principalTable: "tb_tr_price",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_item_tb_tr_transaction_transaction_guid",
                        column: x => x.transaction_guid,
                        principalTable: "tb_tr_transaction",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_role_guid",
                table: "tb_m_employee",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_username",
                table: "tb_m_employee",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_product_guid",
                table: "tb_m_transaction_item",
                column: "product_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_item_transaction_guid",
                table: "tb_m_transaction_item",
                column: "transaction_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_price_guid",
                table: "tb_tr_price",
                column: "price_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_price_product_guid",
                table: "tb_tr_price",
                column: "product_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_transaction_employee_guid",
                table: "tb_tr_transaction",
                column: "employee_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_transaction_item");

            migrationBuilder.DropTable(
                name: "tb_tr_price");

            migrationBuilder.DropTable(
                name: "tb_tr_transaction");

            migrationBuilder.DropTable(
                name: "tb_m_product");

            migrationBuilder.DropTable(
                name: "tb_m_unit");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_role");
        }
    }
}
