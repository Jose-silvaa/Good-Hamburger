using GoodHamburger.Features.Produtos.Dtos;

namespace GoodHamburger.Features.Produtos.Interfaces;

public interface IProdutoService
{
    Task CriarProduto(CriarProdutoDto produtoDto);
    
    Task <IEnumerable<ProdutoDto>> ObterTodosProdutos();
    
}
