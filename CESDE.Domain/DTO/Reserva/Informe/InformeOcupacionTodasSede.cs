using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeOcupacionTodasSede
    {
        public string nombre_sede { get; set; }

        public int ocupacion_total { get; set; }
        
        public List<InformeDia> Dias { get; set; }
    }
}
