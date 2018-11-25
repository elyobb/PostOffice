using Microsoft.EntityFrameworkCore.Migrations;

namespace PostOffice.Migrations
{
    public partial class newfks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tags_Label",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Label",
                table: "Tags",
                column: "Label",
                unique: true,
                filter: "[Label] IS NOT NULL");
        }
    }
}
