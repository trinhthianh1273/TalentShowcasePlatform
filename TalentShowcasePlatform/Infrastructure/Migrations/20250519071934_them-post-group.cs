using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class thempostgroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupPostFeedbackSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalLikes = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalComments = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AverageRating = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    TotalRatings = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPostFeedbackSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupPostFeedbackSummary_GroupPosts_GroupPostId",
                        column: x => x.GroupPostId,
                        principalTable: "GroupPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeCommentGroupPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentGroupPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeCommentGroupPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeCommentGroupPost_CommentGroupPosts_CommentGroupPostId",
                        column: x => x.CommentGroupPostId,
                        principalTable: "CommentGroupPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikeCommentGroupPost_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeGroupPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeGroupPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeGroupPost_GroupPosts_GroupPostId",
                        column: x => x.GroupPostId,
                        principalTable: "GroupPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LikeGroupPost_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingGroupPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingGroupPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingGroupPost_GroupPosts_GroupPostId",
                        column: x => x.GroupPostId,
                        principalTable: "GroupPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingGroupPost_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPostFeedbackSummary_GroupPostId",
                table: "GroupPostFeedbackSummary",
                column: "GroupPostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeCommentGroupPost_CommentGroupPostId",
                table: "LikeCommentGroupPost",
                column: "CommentGroupPostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeCommentGroupPost_UserId_CommentGroupPostId",
                table: "LikeCommentGroupPost",
                columns: new[] { "UserId", "CommentGroupPostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikeGroupPost_GroupPostId",
                table: "LikeGroupPost",
                column: "GroupPostId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeGroupPost_UserId_GroupPostId",
                table: "LikeGroupPost",
                columns: new[] { "UserId", "GroupPostId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingGroupPost_GroupPostId",
                table: "RatingGroupPost",
                column: "GroupPostId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingGroupPost_UserId_GroupPostId",
                table: "RatingGroupPost",
                columns: new[] { "UserId", "GroupPostId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPostFeedbackSummary");

            migrationBuilder.DropTable(
                name: "LikeCommentGroupPost");

            migrationBuilder.DropTable(
                name: "LikeGroupPost");

            migrationBuilder.DropTable(
                name: "RatingGroupPost");
        }
    }
}
