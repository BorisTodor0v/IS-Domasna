using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tickets.Repository.Migrations
{
    public partial class EditOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "OrderItems");
        }
    }
}
