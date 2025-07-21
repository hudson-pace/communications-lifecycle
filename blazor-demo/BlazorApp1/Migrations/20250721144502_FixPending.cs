using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixPending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId");
        }
    }
}
