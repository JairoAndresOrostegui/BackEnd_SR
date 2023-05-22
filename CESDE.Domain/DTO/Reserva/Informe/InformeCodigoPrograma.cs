using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeCodigoPrograma
    {
        public string codigo_programa { get; set; }
        public int cantidad_total_espacios { get; set; }
        public List<InformeDia> Dias { get; set; }
    }
}
