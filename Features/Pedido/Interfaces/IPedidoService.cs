using GoodHamburger.Features.Pedido.Dtos;
using GoodHamburger.Shared;

namespace GoodHamburger.Features.Pedido.Interfaces;

public interface IPedidoService
{
     Task<Result<Models.Pedido>> CriarPedido(CriarPedidoDto dto);

     Task<IEnumerable<PedidoDto>> ObterTodosPedidos();

     Task<Result<Models.Pedido>> ObterPedidoPorId(int id);
     
     Task<Result<Models.Pedido>> ExcluirPedido(int id);
}