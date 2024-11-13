using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureToggle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeatureMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FeatureDB");

            migrationBuilder.CreateTable(
                name: "Business",
                schema: "FeatureDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                schema: "FeatureDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessFeatureFlag",
                schema: "FeatureDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessFeatureFlag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessFeatureFlag_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalSchema: "FeatureDB",
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessFeatureFlag_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "FeatureDB",
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFeatureFlag_BusinessId",
                schema: "FeatureDB",
                table: "BusinessFeatureFlag",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessFeatureFlag_FeatureId",
                schema: "FeatureDB",
                table: "BusinessFeatureFlag",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessFeatureFlag",
                schema: "FeatureDB");

            migrationBuilder.DropTable(
                name: "Business",
                schema: "FeatureDB");

            migrationBuilder.DropTable(
                name: "Feature",
                schema: "FeatureDB");
        }
    }
}
