using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Reporte
{
      public class ReporteDocente
      {
            public string reserva_dia_dia { get; set; }

            public List<ReporteReservaDiaDocente> reporteReservaDiaDocentes { get; set; }
      }
}