using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class vvvvvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Bills");

            migrationBuilder.AlterColumn<bool>(
                name: "BoxId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BoxId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "IsOnline",
                table: "Bills",
                nullable: false,
                defaultValue: 0);
        }
    }
}
