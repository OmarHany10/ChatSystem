using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbSetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_FirstUserId",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_conversations_AspNetUsers_SecondUserId",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_conversations_Groups_GroupId",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_conversations_ConversationId",
                table: "UserConversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_conversations",
                table: "conversations");

            migrationBuilder.RenameTable(
                name: "conversations",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_SecondUserId",
                table: "Conversations",
                newName: "IX_Conversations_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_GroupId",
                table: "Conversations",
                newName: "IX_Conversations_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_conversations_FirstUserId",
                table: "Conversations",
                newName: "IX_Conversations_FirstUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_FirstUserId",
                table: "Conversations",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_SecondUserId",
                table: "Conversations",
                column: "SecondUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Groups_GroupId",
                table: "Conversations",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_FirstUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_SecondUserId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Groups_GroupId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId",
                table: "UserConversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_SecondUserId",
                table: "conversations",
                newName: "IX_conversations_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_GroupId",
                table: "conversations",
                newName: "IX_conversations_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_FirstUserId",
                table: "conversations",
                newName: "IX_conversations_FirstUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_conversations",
                table: "conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_FirstUserId",
                table: "conversations",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_AspNetUsers_SecondUserId",
                table: "conversations",
                column: "SecondUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_conversations_Groups_GroupId",
                table: "conversations",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_conversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId",
                principalTable: "conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
