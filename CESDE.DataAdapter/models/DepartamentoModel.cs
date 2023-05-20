using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("departamento")]
    public class DepartamentoModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_departamento { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_pais { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string nombre_departamento { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string estado_departamento { get; set; }


        public List<MunicipioModel> ForKeyMuniDepartamento { get; set; }
    }
}