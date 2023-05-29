using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.DTO.Reserva
{
    public class ReservaFechaDTO
    {
        public long id_reserva { get; set; }

        public long unidad_organizacional { get; set; }
        public DateTime? fecha_inicio_reserva { get; set; }
        public DateTime? fecha_fin_reserva { get; set; }
    }
}
