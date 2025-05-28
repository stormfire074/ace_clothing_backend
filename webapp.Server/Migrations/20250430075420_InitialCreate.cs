using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExceptionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SAPDatabases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBServerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAPUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAPPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ODBCServer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLayerURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceLayerVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBDriver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateUDFs = table.Column<short>(type: "smallint", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPDatabases", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExceptionLogs");

            migrationBuilder.DropTable(
                name: "SAPDatabases");
        }
    }
}
