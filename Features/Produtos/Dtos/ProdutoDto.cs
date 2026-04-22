using GoodHamburger.Features.Produtos.Models;

namespace GoodHamburger.Features.Produtos.Dtos;

public record ProdutoDto(int Id, string Nome, decimal Preco, TipoProduto TipoProduto);
public record CriarProdutoDto(string Nome, decimal Preco, TipoProduto tipoProduto);

