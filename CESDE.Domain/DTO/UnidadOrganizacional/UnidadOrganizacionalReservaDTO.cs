using System.Collections.Generic;

using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;

namespace CESDE.Domain.DTO.UnidadOrganizacional
{
      public class UnidadOrganizacionalReservaDTO
      {
            public List<ComboReservas> reservas { get; set; }
            public List<ComboDTO> ReservaDisponible { get; set; }
            public List<ComboDTO> ReservaReservada { get; set; }
      }
}