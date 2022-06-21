using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class Xfdfds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsBooked",
                table: "Bills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsOnline",
                table: "Bills",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Bills");
        }
    }
}
