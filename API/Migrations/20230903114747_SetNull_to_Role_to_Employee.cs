using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class SetNull_to_Role_to_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_role_role_guid",
                table: "tb_m_employee");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_guid",
                table: "tb_m_employee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_role_role_guid",
                table: "tb_m_employee",
                column: "role_guid",
                principalTable: "tb_m_role",
                principalColumn: "guid",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_role_role_guid",
                table: "tb_m_employee");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_guid",
                table: "tb_m_employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_role_role_guid",
                table: "tb_m_employee",
                column: "role_guid",
                principalTable: "tb_m_role",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
