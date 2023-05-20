using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("rol_espacio")]
    public class RolEspacioModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_rol_espacio { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_rol { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_tipo_espacio { get; set; }


        public RolModel ForKeyRol_RolEspacio { get; set; }
        public TipoEspacioModel ForKeyTipoEspacio_RolEspacio { get; set; }
    }
}