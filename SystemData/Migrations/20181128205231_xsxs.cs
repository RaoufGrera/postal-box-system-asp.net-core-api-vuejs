using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class xsxs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServesName",
                table: "ExtraCost",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ExtraCost",
                newName: "ServesName");
        }
    }
}
