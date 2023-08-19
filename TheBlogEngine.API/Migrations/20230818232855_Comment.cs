using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBlogEngine.API.Migrations
{
    public partial class Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogList",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogList_BlogId",
                table: "BlogList",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogList_BlogList_BlogId",
                table: "BlogList",
                column: "BlogId",
                principalTable: "BlogList",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogList_BlogList_BlogId",
                table: "BlogList");

            migrationBuilder.DropIndex(
                name: "IX_BlogList_BlogId",
                table: "BlogList");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogList");
        }
    }
}
