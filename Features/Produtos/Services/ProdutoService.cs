using GoodHamburger.Data;
using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Interfaces;
using GoodHamburger.Features.Produtos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoodHamburger.Features.Produtos.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CriarProduto(CriarProdutoDto produtoDto)
    {
        var produto = new Produto
        {
            Nome = produtoDto.Nome,
            Preco = produtoDto.Preco,
            Tipo = produtoDto.tipoProduto
        };
        
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProdutoDto>> ObterTodosProdutos()
    {
        return await _context.Produtos
            .Select(p => new ProdutoDto(
                p.Id,
                p.Nome,
                p.Preco,
                p.Tipo
            ))
            .ToListAsync();
    }
}