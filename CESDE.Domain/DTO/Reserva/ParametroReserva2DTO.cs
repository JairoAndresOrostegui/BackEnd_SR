using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.DTO.Reserva
{
    public class ParametroReserva2DTO
    {
        public long id_unidad_organizacional_padre { get; set; }
        public long id_tipo_espacio { get; set; }
        public DateTime fecha_inicio_reserva { get; set; }
        public DateTime fecha_fin_reserva { get; set; }
        public List<string> reserva_dia_dia { get; set; }
        public List<long> reserva_dia_hora_inicio { get; set; }
        public string estado { get; set; } = string.Empty;
        public long id_caracteristica { get; set; }
        public int capacidad_unidad_organizacional { get; set; }
    }
}
