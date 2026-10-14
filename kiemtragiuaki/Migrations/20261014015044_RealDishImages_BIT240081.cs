using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace kiemtragiuaki.Migrations
{
    /// <inheritdoc />
    public partial class RealDishImages_BIT240081 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 1,
                column: "BIT240081_ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Ph%E1%BB%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg/960px-Ph%E1%BB%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 2,
                column: "BIT240081_ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/4/45/Pho_Bo_by_Banh_%26_Mee_in_Kirkgate_Market.jpg/960px-Pho_Bo_by_Banh_%26_Mee_in_Kirkgate_Market.jpg");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 3,
                column: "BIT240081_ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg/960px-C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 4,
                column: "BIT240081_ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/0/03/Summer_roll.jpg/960px-Summer_roll.jpg");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 5,
                column: "BIT240081_ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/G%E1%BB%8Fi_cu%E1%BB%91n.jpg/960px-G%E1%BB%8Fi_cu%E1%BB%91n.jpg");

            migrationBuilder.InsertData(
                table: "BIT240081_DishImages",
                columns: new[] { "BIT240081_Id", "BIT240081_DishId", "BIT240081_ImageUrl", "BIT240081_IsThumbnail" },
                values: new object[,]
                {
                    { 101, 2, "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/C%C6%A1m_t%E1%BA%A5m_s%C6%B0%E1%BB%9Dn_c%C3%A2y.JPG/960px-C%C6%A1m_t%E1%BA%A5m_s%C6%B0%E1%BB%9Dn_c%C3%A2y.JPG", false },
                    { 102, 3, "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/B%C3%BAn_ch%E1%BA%A3_Vietnamese_food.jpg/960px-B%C3%BAn_ch%E1%BA%A3_Vietnamese_food.jpg", true },
                    { 103, 3, "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Bun-cha-hanoi.jpg/960px-Bun-cha-hanoi.jpg", false },
                    { 104, 5, "https://upload.wikimedia.org/wikipedia/commons/6/6b/Cha_gio.jpg", true },
                    { 105, 5, "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Ch%E1%BA%A3_gi%C3%B2_%28Vietnamese_Spring_Rolls%29_-_12.jpg/960px-Ch%E1%BA%A3_gi%C3%B2_%28Vietnamese_Spring_Rolls%29_-_12.jpg", false },
                    { 106, 7, "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Homemade_Flan.jpg/960px-Homemade_Flan.jpg", true },
                    { 107, 7, "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Cr%C3%A8me_caramel_2.jpg/960px-Cr%C3%A8me_caramel_2.jpg", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 107);

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 1,
                column: "BIT240081_ImageUrl",
                value: "https://picsum.photos/seed/pho1/600/400");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 2,
                column: "BIT240081_ImageUrl",
                value: "https://picsum.photos/seed/pho2/600/400");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 3,
                column: "BIT240081_ImageUrl",
                value: "https://picsum.photos/seed/comtam/600/400");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 4,
                column: "BIT240081_ImageUrl",
                value: "https://picsum.photos/seed/goicuon/600/400");

            migrationBuilder.UpdateData(
                table: "BIT240081_DishImages",
                keyColumn: "BIT240081_Id",
                keyValue: 5,
                column: "BIT240081_ImageUrl",
                value: "https://picsum.photos/seed/goicuon2/600/400");
        }
    }
}
