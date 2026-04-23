using GoodHamburger.Features.Produtos.Dtos;
using GoodHamburger.Features.Produtos.Models;

namespace GoodHamburger.Features.Produtos.Interfaces;

public interface IProdutoService
{
    Task CriarProduto(CriarProdutoDto produtoDto);
    
    Task <IEnumerable<ProdutoDto>> ObterTodosProdutos();
    
    Task<IEnumerable<Produto>> ObterProdutosPorIds(IReadOnlyList<int> produtoIds);
    
}
