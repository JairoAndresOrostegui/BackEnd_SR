using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.DTO.Reserva
{
    public class UnidadComboDTO
    {
        public long id_reserva { get; set; }

        public string nombre_unidad { get; set; }
        public long unidad_organizacional { get; set; }
    }
}