using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureToggle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeatManMig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_User_UserId",
                schema: "featuremanagement",
                table: "Log");

            migrationBuilder.DropIndex(
                name: "IX_Log_UserId",
                schema: "featuremanagement",
                table: "Log");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "featuremanagement",
                table: "Log",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "featuremanagement",
                table: "Log",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserId",
                schema: "featuremanagement",
                table: "Log",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_User_UserId",
                schema: "featuremanagement",
                table: "Log",
                column: "UserId",
                principalSchema: "featuremanagement",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
