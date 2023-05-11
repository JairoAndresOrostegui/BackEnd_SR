namespace CESDE.Domain.Models
{
      public class UnidadOrganizacionalCaracteristica
      {
            public long id_unidad_organizacional_caracteristica { get; set; }
            public long id_caracteristica { get; set; }
            public string nombre_caracteristica { get; set; }
            public long id_unidad_organizacional { get; set; }
            public int cantidad_unidad_organizacional_caracteristica { get; set; }
      }
}