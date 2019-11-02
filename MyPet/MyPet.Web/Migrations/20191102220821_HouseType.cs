using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPet.Web.Migrations
{
    public partial class HouseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Requests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasKids",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasOthePets",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "HouseTypeId",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "Requests",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HouseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HouseTypeId",
                table: "Requests",
                column: "HouseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_HouseTypes_HouseTypeId",
                table: "Requests",
                column: "HouseTypeId",
                principalTable: "HouseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_HouseTypes_HouseTypeId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "HouseTypes");

            migrationBuilder.DropIndex(
                name: "IX_Requests_HouseTypeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "HasKids",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "HasOthePets",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "HouseTypeId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "Requests");
        }
    }
}
