using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeNombreEspacio
    {
        public string nombre_espacio { get; set; }
        public int cantidad_tipoespacio { get; set; }

        public List<InformeDia> Dias { get; set; }
    }
}
