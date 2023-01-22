using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCrawler.Migrations
{
    public partial class EditSomefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "Thumb",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Products",
                newName: "OriginalPrice");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NewPrice",
                table: "Products",
                newName: "ImageLink");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Products",
                newName: "Dimention");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "Thumb");

            migrationBuilder.RenameColumn(
                name: "OriginalPrice",
                table: "Products",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "OldPrice");

            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Products",
                newName: "NewPrice");

            migrationBuilder.RenameColumn(
                name: "Dimention",
                table: "Products",
                newName: "Link");
        }
    }
}
