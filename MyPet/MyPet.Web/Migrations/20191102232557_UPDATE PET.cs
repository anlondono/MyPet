using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPet.Web.Migrations
{
    public partial class UPDATEPET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetImages_PetImageId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Races_RaceId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "PetImages");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PetImageId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_RaceId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "PetImageId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Pets");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Denied",
                table: "Requests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Race",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Denied",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "PetImageId",
                table: "Pets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Pets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PetImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImageUrl = table.Column<string>(nullable: true),
                    PetImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PetImages_PetImages_PetImageId",
                        column: x => x.PetImageId,
                        principalTable: "PetImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetImageId",
                table: "Pets",
                column: "PetImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_RaceId",
                table: "Pets",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PetImages_PetImageId",
                table: "PetImages",
                column: "PetImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetImages_PetImageId",
                table: "Pets",
                column: "PetImageId",
                principalTable: "PetImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Races_RaceId",
                table: "Pets",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
