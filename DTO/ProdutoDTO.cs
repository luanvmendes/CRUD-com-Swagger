using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.DTO
{
    public class ProdutoDTO
    {
        public string Produto { get; set; }
        public int CategoriaId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}