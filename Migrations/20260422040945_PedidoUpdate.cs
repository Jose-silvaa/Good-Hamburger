using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodHamburger.Migrations
{
    /// <inheritdoc />
    public partial class PedidoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Produtos_AcompanhamentoId",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Produtos_BebidaId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_AcompanhamentoId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_BebidaId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "AcompanhamentoId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "BebidaId",
                table: "Pedidos");

            migrationBuilder.CreateTable(
                name: "PedidoProduto",
                columns: table => new
                {
                    AcompanhamentosId = table.Column<int>(type: "integer", nullable: false),
                    PedidoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoProduto", x => new { x.AcompanhamentosId, x.PedidoId });
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoProduto_Produtos_AcompanhamentosId",
                        column: x => x.AcompanhamentosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoProduto_PedidoId",
                table: "PedidoProduto",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoProduto");

            migrationBuilder.AddColumn<int>(
                name: "AcompanhamentoId",
                table: "Pedidos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BebidaId",
                table: "Pedidos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_AcompanhamentoId",
                table: "Pedidos",
                column: "AcompanhamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_BebidaId",
                table: "Pedidos",
                column: "BebidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Produtos_AcompanhamentoId",
                table: "Pedidos",
                column: "AcompanhamentoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Produtos_BebidaId",
                table: "Pedidos",
                column: "BebidaId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
