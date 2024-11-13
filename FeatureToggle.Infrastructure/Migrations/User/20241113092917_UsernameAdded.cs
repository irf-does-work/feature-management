using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureToggle.Infrastructure.Migrations.User
{
    /// <inheritdoc />
    public partial class UsernameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "UserDB",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "UserDB",
                table: "User");
        }
    }
}
