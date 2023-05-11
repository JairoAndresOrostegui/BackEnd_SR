using System.Collections.Generic;

using CESDE.Domain.Models;

namespace CESDE.Domain.DTO.Reserva.Filtros
{
      public class FiltroReservaSubmoduloDTO
      {
            public string nombre { get; set; }
            public List<ReservaDia> reservaDia { get; set; }
      }
}