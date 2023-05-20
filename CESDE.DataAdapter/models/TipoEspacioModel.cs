using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("tipo_espacio")]
    public class TipoEspacioModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_tipo_espacio { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string nombre_tipo_espacio { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string estado_tipo_espacio { get; set; }


        public List<UnidadOrganizacionalModel> ForKeyUnidadOrganiTipoEspacio { get; set; }
        public List<RolEspacioModel> ForKeyRolEspacio_TipoEspacio { get; set; }
    }
}