using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Models;

namespace GoodHamburger.Features.Pedido.Models;

public class Pedido
{
    public int Id { get; set; }
    
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
                    throw new InvalidOperationException("O Pedido já possui um sanduíche.");
                Sanduiche = produto;
                SanduicheId = produto.Id;
                break;
            
            case TipoProduto.Acompanhamento:
                if (_acompanhamentos.Any(a => a.Id == produto.Id))
                    throw new InvalidOperationException($"O acompanhamento '{produto.Nome}' já está no pedido.");
                _acompanhamentos.Add(produto);
                break;
        }
    }

    public decimal Total()
    {
       return (Sanduiche?.Preco ?? 0) + _acompanhamentos.Sum(a => a.Preco);
    }
       
}