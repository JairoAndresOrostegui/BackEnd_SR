using System;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva
{
    public class BuscarDTO
    {
        public long id_reserva { get; set; }
        public string nombre_unidad_organizacional { get; set; }
        public string nombre_submodulo { get; set; }
        public string nombre_usuario_colaborador { get; set; }
        public string nombre_programa { get; set; }
        public string codigo_programa { get; set; }

        public string nombre_rol { get; set; }
        public string area_rol { get; set; }
        public DateTime? fecha_inicio_reserva { get; set; }
        public DateTime? fecha_fin_reserva { get; set; }
        public string descripcion_reserva { get; set; }
        public string estado_reserva { get; set; }

        public List<ReservaDiaDTO> reservaDias { get; set; }
    }
}