using GoodHamburger.Data;
using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Interfaces;
using GoodHamburger.Features.Produtos.Models;

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

    public Task DeletarProduto(int id)
    {
        throw new NotImplementedException();
    }
}