using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureToggle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeatManMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "featuremanagement",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "featuremanagement",
                table: "User");
        }
    }
}
