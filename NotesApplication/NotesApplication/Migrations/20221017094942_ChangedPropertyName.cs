using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApplication.Migrations
{
    public partial class ChangedPropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "color",
                table: "Notes",
                newName: "Color");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Notes",
                newName: "color");
        }
    }
}
