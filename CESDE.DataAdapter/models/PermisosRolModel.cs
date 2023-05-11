using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CESDE.DataAdapter.models
{
      [Table("permisos_rol")]
      public class PermisosRolModel
      {
            [Key]
            [Column(TypeName = "numeric(18, 0)")]
            public long id_permisos_rol { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_funcionalidad { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_rol { get; set; }

            [Column(TypeName = "varchar(2)")]
            public string agregar { get; set; }

            [Column(TypeName = "varchar(2)")]
            public string modificar { get; set; }

            [Column(TypeName = "varchar(2)")]
            public string consultar { get; set; }

            [Column(TypeName = "varchar(2)")]
            public string eliminar { get; set; }

            public RolModel ForKeyRol_Permisos { get; set; }
            public FuncionalidadModel ForKeyFunc_Permisos { get; set; }
      }
}
