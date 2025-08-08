using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommLifecycle.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFieldToCommunicationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CommunicationStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CommunicationStatuses");
        }
    }
}
