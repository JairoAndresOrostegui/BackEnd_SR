using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.DTO.Reserva.Reporte
{
      public class ReporteReservaDiaDocente
      {
            public DateTime? fecha_inicio_reserva { get; set; }
            public DateTime? fecha_fin_reserva { get; set; }
            public string nombre_grupo { get; set; }
            public string nombre_programa { get; set; }
            public string codigo_programa { get; set; }
            public int nivel { get; set; }
            public string nombre_unidad_organizacional { get; set; }
            //public string submodulo_reserva { get; set; }

            public string reserva_dia_hora_inicio { get; set; }
            public string reserva_dia_hora_fin { get; set; }
      }
}