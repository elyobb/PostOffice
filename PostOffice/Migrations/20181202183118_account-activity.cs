using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostOffice.Migrations
{
    public partial class accountactivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copy_Accounts_PostAccountId",
                table: "Copy");

            migrationBuilder.DropIndex(
                name: "IX_Copy_PostAccountId",
                table: "Copy");

            migrationBuilder.DropColumn(
                name: "PostAccountId",
                table: "Copy");

            migrationBuilder.CreateTable(
                name: "AccountActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    CopyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountActivity_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountActivity_Copy_CopyId",
                        column: x => x.CopyId,
                        principalTable: "Copy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountActivity_AccountId",
                table: "AccountActivity",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountActivity_CopyId",
                table: "AccountActivity",
                column: "CopyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountActivity");

            migrationBuilder.AddColumn<int>(
                name: "PostAccountId",
                table: "Copy",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copy_PostAccountId",
                table: "Copy",
                column: "PostAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copy_Accounts_PostAccountId",
                table: "Copy",
                column: "PostAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
