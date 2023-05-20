using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeOcupacionTipoEspacio
    {
        public string nombre_tipoespacio { get; set; }

        public List<InformeOcupacionSede> lsInforme { get; set; }
    }
}
