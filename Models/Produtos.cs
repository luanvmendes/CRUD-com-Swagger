using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public Categoria Categoria { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
    }
}