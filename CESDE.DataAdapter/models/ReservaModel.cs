using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
      [Table("reserva")]
      public class ReservaModel
      {
            [Key]
            [Column(TypeName = "numeric(18, 0)")]
            public long id_reserva { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long id_unidad_organizacional { get; set; }

            [Column(TypeName = "int")]
            public int? identificador_grupo { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string nombre_grupo { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long? id_usuario_reserva { get; set; }

            [Column(TypeName = "datetime")]
            public DateTime? fecha_inicio_reserva { get; set; }

            [Column(TypeName = "datetime")]
            public DateTime? fecha_fin_reserva { get; set; }

            [Column(TypeName = "varchar(100)")]
            public string descripcion_reserva { get; set; }

            [Column(TypeName = "varchar(12)")]
            public string estado_reserva { get; set; }

            [Column(TypeName = "numeric(18, 0)")]
            public long? id_usuario_colaborador { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string nombre_usuario_colaborador { get; set; }

            [Column(TypeName = "int")]
            public int nivel { get; set; }

            [Column(TypeName = "varchar(5)")]
            public string codigo_programa { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string nombre_programa { get; set; }

            //[Column(TypeName = "numeric(18, 0)")]
            //public long? id_rol { get; set; }

            [Column(TypeName = "varchar(250)")]
            public string submodulo { get; set; }


            [Column(TypeName = "varchar(50)")]
            public string jornada { get; set; }

        //[Column(TypeName = "varchar(250)")]
        //public string submodulo_reserva { get; set; }

        public UnidadOrganizacionalModel ForKeyUnidadOrg_Reserva { get; set; }

        public List<ReservaDiaModel> ForKeyReservaDia_Reserva { get; set; }
      }
}