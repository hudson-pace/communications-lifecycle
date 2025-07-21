using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class MakeAuthorIdNullable : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserProfile_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "UserProfile",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
