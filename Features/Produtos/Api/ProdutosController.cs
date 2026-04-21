using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Interfaces;
using GoodHamburger.Features.Produtos.Models;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<ActionResult> CriarProduto([FromBody] CriarProdutoDto produtoDto)
    {
        await _produtoService.CriarProduto(produtoDto);
        
        return Created();
    }
}   