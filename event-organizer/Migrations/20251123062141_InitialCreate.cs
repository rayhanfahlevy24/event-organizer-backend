using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_organizer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TenantType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantPhone = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TenantAdress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BoothNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AreaSm = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantName",
                table: "Tenants",
                column: "TenantName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
