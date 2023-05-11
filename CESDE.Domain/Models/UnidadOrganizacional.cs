using System.Collections.Generic;

namespace CESDE.Domain.Models
{
      public class UnidadOrganizacional
      {
            public long id_unidad_organizacional { get; set; }
            public long id_tipo_espacio { get; set; }
            public long? id_municipio { get; set; }
            public string nombre_unidad_organizacional { get; set; }
            public int piso_unidad_organizacional { get; set; }
            public int capacidad_unidad_organizacional { get; set; }
            public string estado_unidad_organizacional { get; set; }
            public long id_unidad_organizacional_padre { get; set; }

            public List<UnidadOrganizacionalCaracteristica> caracteristicas { get; set; }
      }
}