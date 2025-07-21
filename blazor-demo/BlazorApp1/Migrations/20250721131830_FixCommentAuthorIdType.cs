using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentAuthorIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfile_AuthorUserProfileId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AuthorUserProfileId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "AuthorUserProfileId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Comment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AuthorUserProfileId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AuthorUserProfileId",
                table: "Comment",
                column: "AuthorUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_AuthorUserProfileId",
                table: "Comment",
                column: "AuthorUserProfileId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
