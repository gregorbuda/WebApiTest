using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiTest.Migrations
{
    public partial class updaiden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Identificacion",
                table: "solicitud",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Identificacion",
                table: "solicitud",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
