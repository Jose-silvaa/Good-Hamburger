namespace GoodHamburger.Features.Produtos.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public TipoProduto Tipo { get; set; }
}

public enum TipoProduto
{
    Sanduiche,
    Acompanhamento,
    Bebida
}