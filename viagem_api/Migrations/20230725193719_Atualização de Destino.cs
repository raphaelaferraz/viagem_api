using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viagem_api.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaçãodeDestino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlFoto",
                table: "Destino",
                newName: "UrlFoto2");

            migrationBuilder.AddColumn<string>(
                name: "Meta",
                table: "Destino",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextoDescritivo",
                table: "Destino",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlFoto1",
                table: "Destino",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meta",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "TextoDescritivo",
                table: "Destino");

            migrationBuilder.DropColumn(
                name: "UrlFoto1",
                table: "Destino");

            migrationBuilder.RenameColumn(
                name: "UrlFoto2",
                table: "Destino",
                newName: "UrlFoto");
        }
    }
}
