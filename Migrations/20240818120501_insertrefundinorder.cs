using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nero.Migrations
{
    /// <inheritdoc />
    public partial class insertrefundinorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StripeChargeId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeChargeId",
                table: "Orders");
        }
    }
}
