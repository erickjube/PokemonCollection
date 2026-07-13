using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonCollection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaImportState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastImportedPage = table.Column<int>(type: "int", nullable: false),
                    LastExecution = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportStates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportStates");
        }
    }
}
