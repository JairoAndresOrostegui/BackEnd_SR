using System;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva
{
    public class ParametroReservaDTO
    {
        public long? id_unidad_organizacional_padre { get; set; }
        public long? id_tipo_espacio { get; set; }
        public DateTime? fecha_inicio_reserva { get; set; }
        public DateTime? fecha_fin_reserva { get; set; }
        public List<string> reserva_dia_dia { get; set; }
        public string reserva_dia_hora_inicio { get; set; } = string.Empty;
        public string reserva_dia_hora_fin { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;

        public long? id_caracteristica { get; set; }
        public int? capacidad_unidad_organizacional { get; set; }
    }
}