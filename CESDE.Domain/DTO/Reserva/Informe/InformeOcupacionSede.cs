using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeOcupacionSede
    {
        public string nombre_sede { get; set; }
        public int ocupacion_total { get; set; }
        public List<InformeDia> Dias { get; set; }

    }
}
