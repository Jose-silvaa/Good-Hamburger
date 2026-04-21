using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Models;

namespace GoodHamburger.Features.Pedido.Models;

public class Pedido
{
    public int Id { get; set; }
    
    public int? SanduicheId { get; private set; }
    public Produto? Sanduiche { get; private set; }
    
    public int? AcompanhamentoId { get; private set; }
    public Produto? Acompanhamento { get; private set; }
    
    public int? BebidaId { get; private set; }
    public Produto? Bebida { get; private set; }
    
    public void Adicionar(Produto produto)
    {
        switch (produto.Tipo)
        {
            case TipoProduto.Sanduiche:
                if (Sanduiche is not null)
                    Sanduiche = produto;
                break;
            
            case TipoProduto.Acompanhamento:
                if(Acompanhamento is not null)
                    Acompanhamento = produto;
                break;
            
            case TipoProduto.Bebida:
                if (Bebida is not null);
                    Bebida = produto;
                break;
        }
    }

    public decimal Total()
    {
       return (Sanduiche?.Preco ?? 0) + (Acompanhamento?.Preco ?? 0) + (Bebida?.Preco ?? 0);
    }
       
}