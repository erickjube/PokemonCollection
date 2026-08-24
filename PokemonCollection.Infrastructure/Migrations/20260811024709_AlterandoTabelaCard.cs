using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonCollection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoTabelaCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetPrintedTotal",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetPrintedTotal",
                table: "Cards");
        }
    }
}
