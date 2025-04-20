using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class OnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FriendShip",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FriendShip");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
