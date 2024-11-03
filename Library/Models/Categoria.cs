using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Categoria
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da categoria deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        // Relação com Livro (1:N)
        public ICollection<Livro> Livros { get; set; }
    }
}
