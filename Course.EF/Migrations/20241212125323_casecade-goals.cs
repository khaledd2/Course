using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.DAL.Migrations
{
    /// <inheritdoc />
    public partial class casecadegoals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Course",
                table: "Goal");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Course",
                table: "Goal",
                column: "Course_Id",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_Course",
                table: "Goal");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_Course",
                table: "Goal",
                column: "Course_Id",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
