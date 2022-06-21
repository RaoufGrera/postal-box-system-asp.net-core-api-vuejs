using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Customer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
