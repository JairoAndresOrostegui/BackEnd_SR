using System;
using System.Collections.Generic;

using CESDE.Domain.Models;

namespace CESDE.Domain.DTO.Reserva
{
      public class ReservaDTO
      {
            public long id_reserva { get; set; }
            public string nombre_unidad_organizacional { get; set; }
            public string nombre_submodulo { get; set; }
            public string nombre_usuario_colaborador { get; set; }
            public DateTime? fecha_inicio_reserva { get; set; }
            public DateTime? fecha_fin_reserva { get; set; }
            public string descripcion_reserva { get; set; }
            public string estado_reserva { get; set; }

            public List<ReservaDia> reservaDias { get; set; }
      }
}