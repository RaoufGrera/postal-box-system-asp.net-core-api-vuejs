using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class vvvxvvv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "Office",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedExpret",
                table: "Bills",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Office_RegionId",
                table: "Office",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Region_RegionId",
                table: "Office",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Office_Region_RegionId",
                table: "Office");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Office_RegionId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "BookedExpret",
                table: "Bills");
        }
    }
}
