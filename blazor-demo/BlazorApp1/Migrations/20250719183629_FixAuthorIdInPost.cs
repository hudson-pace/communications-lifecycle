using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class FixAuthorIdInPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_UserProfile_AuthorUserProfileId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AuthorUserProfileId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AuthorUserProfileId",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Post",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorId",
                table: "Post",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_AuthorId",
                table: "Post");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AuthorUserProfileId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_AuthorUserProfileId",
                table: "Post",
                column: "AuthorUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserProfile_AuthorUserProfileId",
                table: "Post",
                column: "AuthorUserProfileId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
