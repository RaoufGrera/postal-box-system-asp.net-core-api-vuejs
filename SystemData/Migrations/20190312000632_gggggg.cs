using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class gggggg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsBooked",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BoxId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsBooked",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "BoxId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
