using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anonym.DataAccess.Migrations.Anonym
{
    public partial class AnonymInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "dbo",
                columns: table => new
                {
                    CategoryId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "dbo",
                columns: table => new
                {
                    PostId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CategoryId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    CreatePostDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                schema: "dbo",
                columns: table => new
                {
                    ChatRoomId = table.Column<string>(nullable: false),
                    ReviewerUserId = table.Column<string>(nullable: true),
                    PostId = table.Column<string>(nullable: false),
                    PublisherName = table.Column<string>(nullable: true),
                    ReviewerName = table.Column<string>(nullable: true),
                    PublisherCommented = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.ChatRoomId);
                    table.ForeignKey(
                        name: "FK_ChatRooms_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "dbo",
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                schema: "dbo",
                columns: table => new
                {
                    ChatMessageId = table.Column<string>(nullable: false),
                    ChatRoomId = table.Column<string>(nullable: false),
                    DisplayUserName = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    IsPublisherMessage = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalSchema: "dbo",
                        principalTable: "ChatRooms",
                        principalColumn: "ChatRoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoomId",
                schema: "dbo",
                table: "ChatMessages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_PostId",
                schema: "dbo",
                table: "ChatRooms",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                schema: "dbo",
                table: "Posts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ChatRooms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "dbo");
        }
    }
}
