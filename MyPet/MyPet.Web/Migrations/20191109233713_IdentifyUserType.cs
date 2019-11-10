using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPet.Web.Migrations
{
    public partial class IdentifyUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdopter",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdopter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "AspNetUsers");
        }
    }
}
