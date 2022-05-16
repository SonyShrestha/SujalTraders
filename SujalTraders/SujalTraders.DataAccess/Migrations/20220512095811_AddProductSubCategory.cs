using Microsoft.EntityFrameworkCore.Migrations;

namespace SujalTraders.Migrations
{
    public partial class AddProductSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "md_product_sub_categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_md_product_sub_categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_md_product_sub_categories_md_product_categories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "md_product_categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_md_product_sub_categories_ProductCategoryId",
                table: "md_product_sub_categories",
                column: "ProductCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "md_product_sub_categories");
        }
    }
}
