using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web;

namespace Library.Models
{
    public class Livro
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da editora não pode ter mais de 100 caracteres.")]
        public string NomeEditora { get; set; }

        [Required(ErrorMessage = "O campo AutorID é obrigatório.")]
        public int AutorID { get; set; }

        [Required(ErrorMessage = "O campo CategoriaID é obrigatório.")]
        public int CategoriaID { get; set; }

        public bool Disponivel { get; set; }

        // Relações com chave estrangeira explícita
        [ForeignKey("AutorID")]
        public Autor Autor { get; set; }

        [ForeignKey("CategoriaID")]
        public Categoria Categoria { get; set; }

        // Caminho da imagem
        [MaxLength(100, ErrorMessage = "O nome da imagem não pode ter mais de 100 caracteres.")]
        public string NomeImagem { get; set; }

        // Propriedade para upload de imagem (não mapeada no banco de dados)
        [NotMapped]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
