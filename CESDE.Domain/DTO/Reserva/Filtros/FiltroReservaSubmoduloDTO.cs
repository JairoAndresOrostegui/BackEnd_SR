using CESDE.Domain.Models;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Filtros
{
    public class FiltroReservaSubmoduloDTO
    {
        public string nombre { get; set; }
        public List<ReservaDia> reservaDia { get; set; }
    }
}