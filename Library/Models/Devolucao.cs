using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models { 
public class Devolucao
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("Reserva")]
    public int ReservaID { get; set; }

    public DateTime DataDevolucao { get; set; }

    // Navegação
    public virtual Reserva Reserva { get; set; }
}
}