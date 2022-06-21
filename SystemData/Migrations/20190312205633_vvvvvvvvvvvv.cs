using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class vvvvvvvvvvvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "NoteTypeBills",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "NoteTypeBills");
        }
    }
}
