using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Reserva
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Livro")]
        [Required(ErrorMessage = "O ID do livro é obrigatório.")]
        public int LivroID { get; set; }

        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public string UsuarioID { get; set; }

        [Required(ErrorMessage = "A data da reserva é obrigatória.")]
        public DateTime DataReserva { get; set; }

        [Required(ErrorMessage = "A data de expiração é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Expiração")]
        public DateTime DataExpiracao { get; set; }

        // Navegação
        public virtual Livro Livro { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public virtual ICollection<Devolucao> Devolucoes { get; set; } // Múltiplas devoluções podem estar associadas
    }
}