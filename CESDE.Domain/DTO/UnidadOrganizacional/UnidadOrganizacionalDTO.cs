using System.Collections.Generic;

namespace CESDE.Domain.DTO.UnidadOrganizacional
{
      public class UnidadOrganizacionalDTO
      {
            public long id_unidad_organizacional { get; set; }
            public string nombre_unidad_organizacional { get; set; }
            public string nombre_tipo_espacio { get; set; }
            public string nombre_municipio { get; set; }
            public string nombre_departamento { get; set; }
            public int piso_unidad_organizacional { get; set; }
            public int capacidad_unidad_organizacional { get; set; }
            public string nombre_unidad_organizacional_padre { get; set; }
            public string estado_unidad_organizacional { get; set; }
            public List<UnidadOrganizacionalCaracteristicaDTO> caracteristicas { get; set; }
      }
}
