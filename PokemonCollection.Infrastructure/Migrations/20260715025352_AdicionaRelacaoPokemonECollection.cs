using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonCollection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRelacaoPokemonECollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "CollectionEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionEntries_PokemonId",
                table: "CollectionEntries",
                column: "PokemonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionEntries_Pokemons_PokemonId",
                table: "CollectionEntries",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectionEntries_Pokemons_PokemonId",
                table: "CollectionEntries");

            migrationBuilder.DropIndex(
                name: "IX_CollectionEntries_PokemonId",
                table: "CollectionEntries");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "CollectionEntries");
        }
    }
}
