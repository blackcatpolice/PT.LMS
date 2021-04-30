using Microsoft.EntityFrameworkCore.Migrations;

namespace PageTechsLMS.DataCore.Data.Mysql.Migrations
{
    public partial class addWxUserinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WxNickName",
                table: "MemberBinds",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WxOpenId",
                table: "MemberBinds",
                type: "longtext CHARACTER SET utf8",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "AspNetUsers",
                type: "longtext CHARACTER SET utf8",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WxNickName",
                table: "MemberBinds");

            migrationBuilder.DropColumn(
                name: "WxOpenId",
                table: "MemberBinds");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "AspNetUsers");
        }
    }
}
