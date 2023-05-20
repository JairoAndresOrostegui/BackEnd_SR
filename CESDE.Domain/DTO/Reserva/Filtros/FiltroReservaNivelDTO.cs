using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Filtros
{
    public class FiltroReservaNivelDTO
    {
        public int nombre { get; set; }
        public int cantidad { get; set; }

        public List<FiltroReservaGrupoDTO> grupos { get; set; }
    }
}