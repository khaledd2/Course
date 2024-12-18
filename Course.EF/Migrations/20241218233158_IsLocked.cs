using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IsLocked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Unit",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Lesson",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Course");
        }
    }
}
