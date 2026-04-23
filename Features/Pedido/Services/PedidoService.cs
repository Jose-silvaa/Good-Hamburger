using GoodHamburger.Data;
using GoodHamburger.Features.Pedido.Dtos;
using GoodHamburger.Features.Pedido.Interfaces;
using GoodHamburger.Features.Produtos.Interfaces;
using GoodHamburger.Features.Produtos.Models;
using GoodHamburger.Shared;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Features.Pedido.Services;

public class PedidoService : IPedidoService
{
    private readonly AppDbContext _context;
    private readonly IProdutoService _produtoService;

    public PedidoService(AppDbContext context,
        IProdutoService produtoService)
    {
        _produtoService = produtoService;
        _context = context;
    }
    
    public async Task<Result<Models.Pedido>> CriarPedido(CriarPedidoDto dto)
    {
        if(dto.ProdutoIds is null || dto.ProdutoIds.Count == 0)
            return Result<Models.Pedido>.Fail("Pedido deve conter ao menos um produto.");
        
        var duplicados = dto.ProdutoIds
            .GroupBy(id => id)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();
        
        if (duplicados.Count > 0)
            return Result<Models.Pedido>.Fail(
                $"Produtos duplicados: {string.Join(", ", duplicados)}");


        var produtos = await _produtoService.ObterProdutosPorIds(dto.ProdutoIds);
        
        var naoEncontrados = dto.ProdutoIds.Except(produtos.Select(p => p.Id)).ToList();

        if (naoEncontrados.Count > 0)
            return Result<Models.Pedido>.Fail($"Produtos não encontrados: {string.Join(", ", naoEncontrados)}");
        
        if(produtos.Count(p => p.Tipo == TipoProduto.Sanduiche) > 1)
            return Result<Models.Pedido>.Fail("O pedido pode conter somente um sanduíche");

        var pedido = new Models.Pedido();
            foreach(var produto in produtos)
                pedido.Adicionar(produto);
        
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();
        
        return Result<Models.Pedido>.Ok(pedido);
    }

    public async Task<IEnumerable<PedidoDto>> ObterTodosPedidos()
    {
        return await _context.Pedidos
            .Select(p => new PedidoDto(
                p.Id,
                p.Sanduiche != null ? p.Sanduiche.Nome : null,
                p.Acompanhamentos
                    .Select(a => a.Nome)
                    .ToList()
                
            ))
            .ToListAsync();
            
    }

    public Task<Result<Models.Pedido>> ObterPedidoPorId(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Models.Pedido>> ExcluirPedido(int id)
    {
        throw new NotImplementedException();
    }
}