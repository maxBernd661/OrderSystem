using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderSystem.Core.Migrations
{
    /// <inheritdoc />
    public partial class OrderCustomerNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Product",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Product");
        }
    }
}
