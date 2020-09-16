using Microsoft.EntityFrameworkCore.Migrations;

namespace YarnStash.Migrations
{
    public partial class PatternCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pattern",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Designer = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    NeedleSize = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pattern", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pattern");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Yarn",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Yarn",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
