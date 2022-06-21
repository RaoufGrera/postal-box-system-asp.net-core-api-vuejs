using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class AddPro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "NoteType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "NoteType");
        }
    }
}
