using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeUnidadesReservadas
    {
        public string nombre_unidad_organizacional { get; set; }
        public int cantidad_reserva { get; set; }

        public int jornada1 { get; set; }
        public int jornada2 { get; set; }
        public int jornada3 { get; set; }
        public int jornada4 { get; set; }
        public int jornada5 { get; set; }
    }
}
