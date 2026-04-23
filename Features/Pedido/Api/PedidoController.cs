using GoodHamburger.Features.Pedido.Dtos;
using GoodHamburger.Features.Pedido.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Features.Pedido.Api;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }
    
    [HttpGet]
    public async Task<IActionResult> ObterTodosPedidos()
    {
        var pedidos = await _pedidoService.ObterTodosPedidos();
        
        return Ok(pedidos);
    }
    
    [HttpPost]
    public async Task<ActionResult> CriarPedido([FromBody] CriarPedidoDto pedidoDto)
    {
        var result = await _pedidoService.CriarPedido(pedidoDto);
        
        if(!result.Success)
            return BadRequest(result.Message);
        
        return Created("",  result.Data);
    }    
    
}