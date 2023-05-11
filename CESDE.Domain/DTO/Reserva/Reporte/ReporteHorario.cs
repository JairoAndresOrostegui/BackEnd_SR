using System;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Reporte
{
      public class ReporteHorario
      {
            public string nombre_grupo { get; set; }
            public DateTime? fecha_inicio_reserva { get; set; }
            public DateTime? fecha_fin_reserva { get; set; }
            public string estado_reserva { get; set; }
            //public string submodulo_reserva { get; set; }
            public int nivel { get; set; }
            public string codigo_programa { get; set; }
            public string nombre_programa { get; set; }


            public List<ReporteReservaDia> reservaDias { get; set; }
      }
}