using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
      [Table("componente")]
      public class ComponenteModel
      {
            [Key]
            [Column(TypeName = "numeric(18, 0)")]
            public long id_componente { get; set; }

            [Column(TypeName = "varchar(50)")]
            public string nombre_componente { get; set; }

            [Column(TypeName = "bit")]
            public bool estado_componente { get; set; }

            public List<FuncionalidadModel> ForKeyFunc_Compo { get; set; }
      }
}