using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("unidad_organizacional_caracteristica")]
    public class UnidadOrganizacionalCaracteristicaModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_unidad_organizacional_caracteristica { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_caracteristica { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_unidad_organizacional { get; set; }

        [Column(TypeName = "int")]
        public int cantidad_unidad_organizacional_caracteristica { get; set; }


        public CaracteristicaModel ForKeyCaracteristica_UOC { get; set; }
        public UnidadOrganizacionalModel ForKeyUnidadOrgani_UOC { get; set; }
    }
}