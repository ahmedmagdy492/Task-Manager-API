using Microsoft.EntityFrameworkCore.Migrations;

namespace Task_Manager.Migrations
{
    public partial class modifedWorkTaskTblRelationWithUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkTasks",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkTasks_UserId",
                table: "WorkTasks",
                newName: "IX_WorkTasks_CreatorId");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                table: "WorkTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_AssignedToId",
                table: "WorkTasks",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_AssignedToId",
                table: "WorkTasks",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_CreatorId",
                table: "WorkTasks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_AssignedToId",
                table: "WorkTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTasks_Users_CreatorId",
                table: "WorkTasks");

            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_AssignedToId",
                table: "WorkTasks");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "WorkTasks");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "WorkTasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkTasks_CreatorId",
                table: "WorkTasks",
                newName: "IX_WorkTasks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTasks_Users_UserId",
                table: "WorkTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
