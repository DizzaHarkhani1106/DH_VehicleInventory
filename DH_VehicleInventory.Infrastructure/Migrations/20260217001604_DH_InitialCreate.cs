using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DH_VehicleInventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DH_InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DH_Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DH_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_DH_Vehicles_Status",
                table: "DH_Vehicles",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IDX_DH_Vehicles_VehicleCode",
                table: "DH_Vehicles",
                column: "VehicleCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DH_Vehicles");
        }
    }
}
