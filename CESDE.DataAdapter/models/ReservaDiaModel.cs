using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("reserva_dia")]
    public class ReservaDiaModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long reserva_dia_id { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_reserva { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string reserva_dia_dia { get; set; }

        [Column(TypeName = "int")]
        public int reserva_dia_hora_inicio { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string jornada { get; set; }

        //[Column(TypeName = "varchar(50)")]
        //public string reserva_dia_hora_fin { get; set; }

        public ReservaModel ForKeyReserva_ReservaDia { get; set; }
    }
}