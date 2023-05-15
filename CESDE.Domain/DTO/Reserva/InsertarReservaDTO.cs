using System;
using System.Collections.Generic;

using CESDE.Domain.Models;

namespace CESDE.Domain.DTO.Reserva
{
      public class InsertarReservaDTO
      {
            public long id_reserva { get; set; }
            public long id_unidad_organizacional { get; set; }
            public int identificador_grupo { get; set; }
            public string nombre_grupo { get; set; }
            public long id_usuario_reserva { get; set; }
            public DateTime fecha_inicio_reserva { get; set; }
            public DateTime fecha_fin_reserva { get; set; }
            public string descripcion_reserva { get; set; }
            public string estado_reserva { get; set; }
            public long id_usuario_colaborador { get; set; }
            public string nombre_usuario_colaborador { get; set; }
            public int nivel { get; set; }
            public string codigo_programa { get; set; }
            public string nombre_programa { get; set; }
            //public long id_rol { get; set; }
            public string jornada { get; set; }
            public string submodulo { get; set; }

            public string sede { get; set; }


            public ReservaDiaDTO reservaDia { get; set; }
      }
}
