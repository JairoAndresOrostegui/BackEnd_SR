using System.Collections.Generic;

namespace CESDE.Domain.DTO.Reserva.Informe
{
    public class InformeOcupacionSede
    {
        public int ocupacion_total { get; set; }
        public int lunes_conteo_jornada1 { get; set; }
        public int lunes_conteo_jornada2 { get; set; }
        public int lunes_conteo_jornada3 { get; set; }

        public int martes_conteo_jornada1 { get; set; }
        public int martes_conteo_jornada2 { get; set; }
        public int martes_conteo_jornada3 { get; set; }

        public int miercoles_conteo_jornada1 { get; set; }
        public int miercoles_conteo_jornada2 { get; set; }
        public int miercoles_conteo_jornada3 { get; set; }

        public int jueves_conteo_jornada1 { get; set; }
        public int jueves_conteo_jornada2 { get; set; }
        public int jueves_conteo_jornada3 { get; set; }

        public int viernes_conteo_jornada1 { get; set; }
        public int viernes_conteo_jornada2 { get; set; }
        public int viernes_conteo_jornada3 { get; set; }

        public int sadado_conteo_jornada4 { get; set; }
        public int domingo_conteo_jornada5 { get; set; }

    }
}
