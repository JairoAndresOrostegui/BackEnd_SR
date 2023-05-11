using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
      [Table("unidad_organizacional")]
      public class UnidadOrganizacionalModel
      {
            [Key]
            [Column(TypeName = "numeric(18, 0)")]
            public long id_unidad_organizacional { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_tipo_espacio { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long? id_municipio { get; set; }

            [Column(TypeName = "varchar(150)")]
            public string nombre_unidad_organizacional { get; set; }

            [Column(TypeName = "int")]
            public int piso_unidad_organizacional { get; set; }

            [Column(TypeName = "int")]
            public int capacidad_unidad_organizacional { get; set; }

            [Column(TypeName = "varchar(12)")]
            public string estado_unidad_organizacional { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_unidad_organizacional_padre { get; set; }


            public List<UnidadOrganizacionalCaracteristicaModel> ForKeyUOC_UnidadOrgani { get; set; }
            public TipoEspacioModel ForKeyTipoEspacioUnidad { get; set; }
            public MunicipioModel ForKeyMunicipio_Unidad { get; set; }

            public List<UnidadRolModel> ForKeyUnidadRol_UnidadOrgani { get; set; }
            public List<ReservaModel> ForKeyReserva_UnidadOrg { get; set; }


            public List<UsuarioModel> ForKeyUsuario_Unidad { get; set; }
        //public List<ProgramaModel> ForKeyPrograma_UnidadOrg { get; set; }

        //public List<VinculacionModel> ForKeyVinvulacion_UnidadOrgan { get; set; }
    }
}