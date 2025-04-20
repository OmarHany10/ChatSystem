using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAcceptedtoFriendShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Friendships",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Friendships");
        }
    }
}
