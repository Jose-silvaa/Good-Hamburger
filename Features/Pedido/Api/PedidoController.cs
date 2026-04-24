using GoodHamburger.Features.Pedido.Dtos;
using GoodHamburger.Features.Pedido.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(
        Summary = "Buscar todos os pedidos existentes"
    )]
    public async Task<IActionResult> ObterTodosPedidos()
    {
        var pedidos = await _pedidoService.ObterTodosPedidos();
        
        return Ok(pedidos);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Description = @" Cria um pedido com base em uma lista de IDs de produtos. Cada ID representa um produto existente no sistema. IDs disponíveis:
        1 = X Burger,
        2 = X Egg,
        3 = X Bacon,
        4 = Batata frita,
        5 = Refrigerante",
        Summary = "Criar um novo pedido"
    )]
    [SwaggerResponse(201, "Pedido criado com sucesso")]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<ActionResult> CriarPedido([FromBody] CriarPedidoDto pedidoDto)
    {
        var result = await _pedidoService.CriarPedido(pedidoDto);
        
        if(!result.Success)
            return BadRequest(result.Message);
        
        return Created("",  result.Data);
    }

    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Description = @"Exclui um pedido baseado no id",
        Summary = "Excluir um pedido existente"
    )]
    public async Task<ActionResult> ExcluirPedido(int id)
    {
        var result = await _pedidoService.ExcluirPedido(id);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok("Pedido excluido com sucesso");
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Buscar um pedido existente pelo o ID"
    )]
    public async Task<ActionResult> ObterPedidoPorId(int id)
    {
        var result = await _pedidoService.ObterPedidoPorId(id);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
    
    [HttpPut("{pedidoId}")]
    [SwaggerOperation(
        Summary = "Atualizar o pedido pelo o ID",
        Description = "É necessário informar um objeto no body com as propriedades ProdutoIdsParaAdicionar e ProdutoIdsParaRemover passando os respectivos ID's no array"
    )]
    public async Task<IActionResult> AtualizarPedido(int pedidoId, [FromBody] AtualizarPedidoDto dto)
    {
        var result = await _pedidoService.AtualizarPedido(pedidoId, dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Data);
    }
    
}