using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemData.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PayType_PayTypeId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "PayTypeId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceKey",
                table: "Bills",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NoteType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteTypeBills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NoteTypeId = table.Column<int>(nullable: false),
                    BillsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTypeBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteTypeBills_Bills_BillsId",
                        column: x => x.BillsId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteTypeBills_NoteType_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "NoteType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTypeBills_BillsId",
                table: "NoteTypeBills",
                column: "BillsId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteTypeBills_NoteTypeId",
                table: "NoteTypeBills",
                column: "NoteTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PayType_PayTypeId",
                table: "Bills",
                column: "PayTypeId",
                principalTable: "PayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PayType_PayTypeId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "NoteTypeBills");

            migrationBuilder.DropTable(
                name: "NoteType");

            migrationBuilder.DropColumn(
                name: "PriceKey",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "PayTypeId",
                table: "Bills",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PayType_PayTypeId",
                table: "Bills",
                column: "PayTypeId",
                principalTable: "PayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
