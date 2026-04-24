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
        var validacao = await ObterEValidarProdutosAsync(dto.ProdutoIds);
        if (!validacao.Success)
            return Result<Models.Pedido>.Fail(validacao.Message);

        var pedido = new Models.Pedido();

        AplicarProdutos(pedido, validacao.Data);

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
                    .ToList(),
                p.Subtotal,
                p.Total
            ))
            .ToListAsync();
            
    }

    public async Task<Result<PedidoDto>> ObterPedidoPorId(int id)
    {
        var pedido = await _context.Pedidos
            .Where(p => p.Id == id)
            .Select(p => new PedidoDto(
                p.Id,
                p.Sanduiche != null ? p.Sanduiche.Nome : null,
                p.Acompanhamentos
                    .Select(a => a.Nome)
                    .ToList(),
                p.Subtotal,
                p.Total
            ))
            .FirstOrDefaultAsync();

        if (pedido is null)
           return Result<PedidoDto>.Fail($"O pedido com o ID: {id} não existe");

        return Result<PedidoDto>.Ok(pedido);
    }

    public async Task<Result<Models.Pedido>> ExcluirPedido(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido is null)
            return Result<Models.Pedido>.Fail("O pedido não existe");
        _context.Pedidos.Remove(pedido);
        
        await _context.SaveChangesAsync();
        return Result<Models.Pedido>.Ok(pedido);
    }
    
    public async Task<Result<Models.Pedido>> AtualizarPedido(int pedidoId, AtualizarPedidoDto dto)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Sanduiche)
            .Include(p => p.Acompanhamentos)
            .FirstOrDefaultAsync(p => p.Id == pedidoId);

        if (pedido is null)
            return Result<Models.Pedido>.Fail("Pedido não encontrado.");

        Result<List<Produto>> produtosParaAdicionar = null;
        Result<List<Produto>> produtosParaRemover = null;

        if (dto.ProdutoIdsParaAdicionar?.Any() == true)
        {
            produtosParaAdicionar = await ObterEValidarProdutosAsync(dto.ProdutoIdsParaAdicionar);
            if (!produtosParaAdicionar.Success)
                return Result<Models.Pedido>.Fail(produtosParaAdicionar.Message);
            
            foreach (var produto in produtosParaAdicionar.Data)
                pedido.Adicionar(produto);
        }
        
        if (dto.ProdutoIdsParaRemover?.Any() == true)
        {
            produtosParaRemover = await ObterEValidarProdutosAsync(dto.ProdutoIdsParaRemover);
            if (!produtosParaRemover.Success)
                return Result<Models.Pedido>.Fail(produtosParaRemover.Message);
            
            pedido.RemoverProdutos(produtosParaRemover.Data);
        }
        
        pedido.CalcularSubTotal();
        pedido.CalcularTotal();
        
        await _context.SaveChangesAsync();

        return Result<Models.Pedido>.Ok(pedido);
    }
    
 
    public async Task<Result<List<Produto>>> ObterEValidarProdutosAsync(IReadOnlyList<int> ids)
    {
        if (!ids.Any())
            return Result<List<Produto>>.Fail("Nenhum produto informado.");
        
        var duplicados = ids
            .GroupBy(id => id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicados.Any())
            return Result<List<Produto>>.Fail(
                $"Produtos duplicados: {string.Join(", ", duplicados)}");

        var produtos = (await _produtoService.ObterProdutosPorIds(ids.ToList())).ToList();

        var naoEncontrados = ids.Except(produtos.Select(p => p.Id)).ToList();

        if (naoEncontrados.Any())
            return Result<List<Produto>>.Fail(
                $"Produtos não encontrados: {string.Join(", ", naoEncontrados)}");

        if (produtos.Count(p => p.Tipo == TipoProduto.Sanduiche) > 1)
            return Result<List<Produto>>.Fail("O pedido pode conter somente um sanduíche");

        return Result<List<Produto>>.Ok(produtos);
    }
    
    public void AplicarProdutos(Models.Pedido pedido, List<Produto> produtos)
    {
        foreach (var produto in produtos)
            pedido.Adicionar(produto);

        pedido.CalcularSubTotal();
        pedido.CalcularTotal();
    }
}