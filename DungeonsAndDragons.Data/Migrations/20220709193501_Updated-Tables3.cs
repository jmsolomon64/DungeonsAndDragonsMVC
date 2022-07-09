using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DungeonsAndDragons.Data.Migrations
{
    public partial class UpdatedTables3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Races",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Races");
        }
    }
}
