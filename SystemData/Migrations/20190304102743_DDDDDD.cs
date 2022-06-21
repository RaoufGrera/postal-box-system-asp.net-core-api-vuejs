using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class DDDDDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneAuth",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneAuth",
                table: "AspNetUsers");
        }
    }
}
