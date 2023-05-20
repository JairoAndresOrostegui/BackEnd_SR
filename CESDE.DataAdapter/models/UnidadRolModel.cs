using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("unidad_rol")]
    public class UnidadRolModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_unidad_rol { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_unidad_organizacional { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_rol { get; set; }


        public RolModel ForKeyRol_UnidadRol { get; set; }
        public UnidadOrganizacionalModel ForKeyUnidadOrgani_UnidadRol { get; set; }
    }
}