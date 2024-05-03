using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilityLibrary.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    u_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    u_login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    u_register_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.u_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
