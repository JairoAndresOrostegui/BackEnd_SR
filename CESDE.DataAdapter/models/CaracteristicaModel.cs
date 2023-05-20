using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("caracteristica")]
    public class CaracteristicaModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_caracteristica { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string nombre_caracteristica { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string estado_caracteristica { get; set; }

        public List<UnidadOrganizacionalCaracteristicaModel> ForKeyUOC_Caracteristica { get; set; }
    }
}