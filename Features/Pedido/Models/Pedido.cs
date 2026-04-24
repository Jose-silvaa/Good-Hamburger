using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Models;
using GoodHamburger.Shared;

namespace GoodHamburger.Features.Pedido.Models;

public class Pedido
{
    public int Id { get; set; }
    
    public decimal Subtotal { get; private set; }
    
    public decimal Total { get; private set; }
    
    public int? SanduicheId { get; private set; }
    public Produto? Sanduiche { get; private set; }
    
    private readonly List<Produto> _acompanhamentos = new();
    public IReadOnlyList<Produto> Acompanhamentos => _acompanhamentos;
    
    public void Adicionar(Produto produto)
    {
        switch (produto.Tipo)
        {
            case TipoProduto.Sanduiche:
                if (Sanduiche is not null)
                    Result<Produto>.Fail("O pedido já possui um sanduíche.");
                Sanduiche = produto;
                SanduicheId = produto.Id;
                break;
            
            case TipoProduto.Acompanhamento:
                if (_acompanhamentos.Any(a => a.Id == produto.Id))
                    Result<Produto>.Fail($"O acompanhamento '{produto.Nome}' já está no pedido.");
                _acompanhamentos.Add(produto);
                break;
        }
    }

    public void CalcularSubTotal()
    {
       Subtotal = (Sanduiche?.Preco ?? 0) + _acompanhamentos.Sum(a => a.Preco);
    }

    public void CalcularTotal()
    {
        decimal subtotal = 0m;

        if (Sanduiche != null)
            subtotal += Sanduiche.Preco;

        subtotal += _acompanhamentos.Sum(a => a.Preco);

        decimal desconto = CalcularDesconto(subtotal);
        
        Total = subtotal - desconto;
    }

    public decimal CalcularDesconto(decimal subtotal)
    {
        bool temSanduiche = Sanduiche != null;
        bool temBatata = _acompanhamentos.Any(a => a.Nome == "Batata frita");
        bool temRefrigerante = _acompanhamentos.Any(a => a.Nome == "Refrigerante");
        
        if (temSanduiche && temBatata && temRefrigerante)
            return subtotal * 0.20m;

        if (temSanduiche && temRefrigerante)
            return subtotal * 0.15m;

        if (temSanduiche && temBatata)
            return subtotal * 0.10m;

        return 0m;
    }
    
    public void RemoverProdutos(IEnumerable<Produto> produtos)
    {
        var ids = produtos.Select(p => p.Id).ToHashSet();

        if (SanduicheId.HasValue && ids.Contains(SanduicheId.Value))
        {
            SanduicheId = null;
            Sanduiche = null;
        }

        _acompanhamentos.RemoveAll(p => ids.Contains(p.Id));
    }

}