using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductivityApp.Migrations
{
    public partial class addordering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {          

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Field",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Criteria",
                nullable: false,
                defaultValue: 0);

                migrationBuilder.AddColumn<int>(
                  name: "Order",
                  table: "Answer",
                  nullable: false,
                  defaultValue:0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Field");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Answer");
            
        }
    }
}
