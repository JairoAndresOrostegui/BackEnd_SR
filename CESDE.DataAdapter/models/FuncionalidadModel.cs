using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("funcionalidad")]
    public class FuncionalidadModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_funcionalidad { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_componente { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string nombre_funcionalidad { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string url_funcionalidad { get; set; }

        [Column(TypeName = "bit")]
        public bool estado_funcionalidad { get; set; }


        public List<PermisosRolModel> ForKeyPermisos_Func { get; set; }
        public ComponenteModel ForKeyCompo_Func { get; set; }
    }
}