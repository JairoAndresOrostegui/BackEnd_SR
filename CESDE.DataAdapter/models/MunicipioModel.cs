using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
      [Table("municipio")]
      public class MunicipioModel
      {
            [Key]
            [Column(TypeName = "numeric(18, 0)")]
            public long id_municipio { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_departamento { get; set; }

            [Column(TypeName = "varchar(50)")]
            public string nombre_municipio { get; set; }

            [Column(TypeName = "varchar(12)")]
            public string estado_municipio { get; set; }

            //public List<IdentificacionModel> ForKeyIdentificacion_Municipio { get; set; }

            public DepartamentoModel ForKeyDepartamentoMuni { get; set; }
            public List<UnidadOrganizacionalModel> ForKeyUnidad_Municipio { get; set; }
            //public List<PersonaModel> ForKeyPersonMunicipioResidencia { get; set; }
      }
}