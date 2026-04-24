using GoodHamburger.Features.Pedido.Dtos;
using GoodHamburger.Features.Produtos.Models;
using GoodHamburger.Shared;

namespace GoodHamburger.Features.Pedido.Interfaces;

public interface IPedidoService
{
     Task<Result<Models.Pedido>> CriarPedido(CriarPedidoDto dto);

     Task<IEnumerable<PedidoDto>> ObterTodosPedidos();

     Task<Result<PedidoDto>> ObterPedidoPorId(int id);
     
     Task<Result<Models.Pedido>> ExcluirPedido(int id);
     Task<Result<Models.Pedido>> AtualizarPedido(int pedidoId, AtualizarPedidoDto dto);
     Task<Result<List<Produto>>> ObterEValidarProdutosAsync(IReadOnlyList<int> ids);

     void AplicarProdutos(Models.Pedido pedido, List<Produto> produtos);
}