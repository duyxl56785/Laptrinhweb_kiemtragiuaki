using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kiemtragiuaki.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_BIT240081 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BIT240081_DishCategories",
                columns: table => new
                {
                    BIT240081_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BIT240081_Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BIT240081_Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240081_DishCategories", x => x.BIT240081_Id);
                });

            migrationBuilder.CreateTable(
                name: "BIT240081_Dishes",
                columns: table => new
                {
                    BIT240081_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BIT240081_Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BIT240081_Price = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    BIT240081_PreparationTime = table.Column<int>(type: "integer", nullable: false),
                    BIT240081_IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    BIT240081_Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BIT240081_DishCategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240081_Dishes", x => x.BIT240081_Id);
                    table.CheckConstraint("CK_BIT240081_Dishes_PreparationTime", "\"BIT240081_PreparationTime\" > 0");
                    table.CheckConstraint("CK_BIT240081_Dishes_Price", "\"BIT240081_Price\" > 0");
                    table.ForeignKey(
                        name: "FK_BIT240081_Dishes_BIT240081_DishCategories_BIT240081_DishCat~",
                        column: x => x.BIT240081_DishCategoryId,
                        principalTable: "BIT240081_DishCategories",
                        principalColumn: "BIT240081_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BIT240081_DishImages",
                columns: table => new
                {
                    BIT240081_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BIT240081_ImageUrl = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    BIT240081_IsThumbnail = table.Column<bool>(type: "boolean", nullable: false),
                    BIT240081_DishId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIT240081_DishImages", x => x.BIT240081_Id);
                    table.ForeignKey(
                        name: "FK_BIT240081_DishImages_BIT240081_Dishes_BIT240081_DishId",
                        column: x => x.BIT240081_DishId,
                        principalTable: "BIT240081_Dishes",
                        principalColumn: "BIT240081_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BIT240081_DishCategories",
                columns: new[] { "BIT240081_Id", "BIT240081_Description", "BIT240081_Name" },
                values: new object[,]
                {
                    { 1, "Các món ăn chính", "Món chính" },
                    { 2, "Món khai vị", "Khai vị" },
                    { 3, "Món tráng miệng", "Tráng miệng" }
                });

            migrationBuilder.InsertData(
                table: "BIT240081_Dishes",
                columns: new[] { "BIT240081_Id", "BIT240081_Description", "BIT240081_DishCategoryId", "BIT240081_IsAvailable", "BIT240081_Name", "BIT240081_PreparationTime", "BIT240081_Price" },
                values: new object[,]
                {
                    { 1, "Phở bò tái chín truyền thống", 1, true, "Phở bò", 20, 55000m },
                    { 2, "Cơm tấm sườn nướng, bì, chả", 1, true, "Cơm tấm sườn", 15, 45000m },
                    { 3, "Bún chả Hà Nội", 1, false, "Bún chả", 25, 50000m },
                    { 4, "Gỏi cuốn tôm thịt", 2, true, "Gỏi cuốn", 10, 30000m },
                    { 5, "Chả giò chiên giòn", 2, true, "Chả giò", 18, 35000m },
                    { 6, "Chè đậu, thạch, nước cốt dừa", 3, true, "Chè ba màu", 5, 20000m },
                    { 7, "Bánh flan caramen", 3, false, "Bánh flan", 30, 15000m }
                });

            migrationBuilder.InsertData(
                table: "BIT240081_DishImages",
                columns: new[] { "BIT240081_Id", "BIT240081_DishId", "BIT240081_ImageUrl", "BIT240081_IsThumbnail" },
                values: new object[,]
                {
                    { 1, 1, "https://picsum.photos/seed/pho1/600/400", true },
                    { 2, 1, "https://picsum.photos/seed/pho2/600/400", false },
                    { 3, 2, "https://picsum.photos/seed/comtam/600/400", true },
                    { 4, 4, "https://picsum.photos/seed/goicuon/600/400", true },
                    { 5, 4, "https://picsum.photos/seed/goicuon2/600/400", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIT240081_DishCategories_BIT240081_Name",
                table: "BIT240081_DishCategories",
                column: "BIT240081_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BIT240081_Dishes_BIT240081_DishCategoryId_BIT240081_Name",
                table: "BIT240081_Dishes",
                columns: new[] { "BIT240081_DishCategoryId", "BIT240081_Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BIT240081_DishImages_OneThumbnail",
                table: "BIT240081_DishImages",
                column: "BIT240081_DishId",
                unique: true,
                filter: "\"BIT240081_IsThumbnail\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BIT240081_DishImages");

            migrationBuilder.DropTable(
                name: "BIT240081_Dishes");

            migrationBuilder.DropTable(
                name: "BIT240081_DishCategories");
        }
    }
}
