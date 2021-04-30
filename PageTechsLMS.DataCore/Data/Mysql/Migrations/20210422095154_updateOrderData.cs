using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PageTechsLMS.DataCore.Data.Mysql.Migrations
{
    public partial class updateOrderData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Courses_CourseId",
                table: "Tags");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Tags",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderChannel",
                table: "Paylogs",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderFee",
                table: "Paylogs",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "Paylogs",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "Paylogs",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fee",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderType",
                table: "CourseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OutTradeNo",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayChannel",
                table: "CourseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeId",
                table: "CourseOrder",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    SiteName = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Keys = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    Description = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    FooterScript = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true),
                    FooterInfo = table.Column<string>(type: "longtext CHARACTER SET utf8", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Courses_CourseId",
                table: "Tags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Courses_CourseId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropColumn(
                name: "OrderChannel",
                table: "Paylogs");

            migrationBuilder.DropColumn(
                name: "OrderFee",
                table: "Paylogs");

            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "Paylogs");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Paylogs");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "OutTradeNo",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "PayChannel",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "CourseOrder");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "CourseOrder");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Tags",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Courses_CourseId",
                table: "Tags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
