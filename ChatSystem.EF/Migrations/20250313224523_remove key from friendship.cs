using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class removekeyfromfriendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "FriendShip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FriendShip",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
