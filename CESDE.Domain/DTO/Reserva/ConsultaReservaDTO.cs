using CESDE.Domain.Models;
using System;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva
{
    public class ConsultaReservaDTO
    {
        public long id_unidad_organizacional { get; set; }
        public DateTime? fecha_inicio_reserva { get; set; }
        public DateTime? fecha_fin_reserva { get; set; }

        public List<ReservaDia> reservaDias { get; set; }
    }
}