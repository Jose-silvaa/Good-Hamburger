using System.ComponentModel.DataAnnotations;
using GoodHamburger.Features.Produtos.Models;

namespace GoodHamburger.Features.Pedido.Dtos;

public record CriarPedidoDto(
     [Required] 
     [MinLength(1, ErrorMessage = "O pedido deve conter ao menos um produto")]
     IReadOnlyList<int> ProdutoIds
    );
    
    public record PedidoDto(int Id, string? Sanduiche, List<string> Acompanhamentos);