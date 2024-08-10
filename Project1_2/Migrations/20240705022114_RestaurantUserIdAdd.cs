using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project1_2.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantUserIdAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_CreatedById",
                table: "restaurants",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_restaurants_Users_CreatedById",
                table: "restaurants",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_restaurants_Users_CreatedById",
                table: "restaurants");

            migrationBuilder.DropIndex(
                name: "IX_restaurants_CreatedById",
                table: "restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "restaurants");
        }
    }
}
