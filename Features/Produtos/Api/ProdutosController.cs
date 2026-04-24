using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GoodHamburger.Features.Produtos.Api;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Buscar todos os pedidos existentes"
    )]
    public async Task<IActionResult> ObterTodosProdutos()
    {
        var produtos = await _produtoService.ObterTodosProdutos();
        
        return Ok(produtos);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Criar um novo produto"
    )]
    public async Task<ActionResult> CriarProduto([FromBody] CriarProdutoDto produtoDto)
    {
        await _produtoService.CriarProduto(produtoDto);
        return Created();
    }
}   