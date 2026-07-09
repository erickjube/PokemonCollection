using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonCollection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaTabelaCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PokemonId",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Cards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SetCode",
                table: "Cards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "SetCode",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonId",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
