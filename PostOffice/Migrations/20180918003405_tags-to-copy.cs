using Microsoft.EntityFrameworkCore.Migrations;

namespace PostOffice.Migrations
{
    public partial class tagstocopy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copy_PostItems_PostItemId",
                table: "Copy");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_PostItems_PostItemId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PostItemId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PostItemId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CopyId",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "PostItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostItemId",
                table: "Copy",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CopyId",
                table: "Tags",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Label",
                table: "Tags",
                column: "Label",
                unique: true,
                filter: "[Label] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PostItems_Url",
                table: "PostItems",
                column: "Url",
                unique: true,
                filter: "[Url] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Label",
                table: "Accounts",
                column: "Label",
                unique: true,
                filter: "[Label] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Copy_PostItems_PostItemId",
                table: "Copy",
                column: "PostItemId",
                principalTable: "PostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Copy_CopyId",
                table: "Tags",
                column: "CopyId",
                principalTable: "Copy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copy_PostItems_PostItemId",
                table: "Copy");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Copy_CopyId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CopyId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Label",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_PostItems_Url",
                table: "PostItems");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Label",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CopyId",
                table: "Tags");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Tags",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostItemId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "PostItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostItemId",
                table: "Copy",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostItemId",
                table: "Tags",
                column: "PostItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copy_PostItems_PostItemId",
                table: "Copy",
                column: "PostItemId",
                principalTable: "PostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_PostItems_PostItemId",
                table: "Tags",
                column: "PostItemId",
                principalTable: "PostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
