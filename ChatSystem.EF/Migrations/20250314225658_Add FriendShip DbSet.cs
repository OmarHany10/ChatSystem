using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendShipDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_AspNetUsers_FriendID",
                table: "FriendShip");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip");

            migrationBuilder.RenameTable(
                name: "FriendShip",
                newName: "Friendships");

            migrationBuilder.RenameIndex(
                name: "IX_FriendShip_UserId",
                table: "Friendships",
                newName: "IX_Friendships_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                columns: new[] { "FriendID", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_FriendID",
                table: "Friendships",
                column: "FriendID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId",
                table: "Friendships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_FriendID",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_UserId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "FriendShip");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_UserId",
                table: "FriendShip",
                newName: "IX_FriendShip_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendShip",
                table: "FriendShip",
                columns: new[] { "FriendID", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_AspNetUsers_FriendID",
                table: "FriendShip",
                column: "FriendID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendShip_AspNetUsers_UserId",
                table: "FriendShip",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
