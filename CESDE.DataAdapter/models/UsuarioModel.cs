using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
    [Table("usuario")]
    public class UsuarioModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_usuario { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_rol { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_unidad_organizacional { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public long id_persona { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string login_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_inicio_autenticacion_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_fin_autenticacion_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_actualizacion_clave_usuario { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_actualizacion_datos_usuario { get; set; }

        [Column(TypeName = "int")]
        public int numero_intentos_autenticacion_usuario { get; set; }

        [Column(TypeName = "varchar(18)")]
        public string estado_datos_usuario { get; set; }

        [Column(TypeName = "varchar(18)")]
        public string estado_clave_usuario { get; set; }

        [Column(TypeName = "varchar(18)")]
        public string estado_usuario { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string clave_usuario { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string correo_institucional_usuario { get; set; }


        public PersonaModel ForKeyPersona { get; set; }
        public RolModel ForKeyRol_Usuario { get; set; }
        public UnidadOrganizacionalModel ForKeyUnidad_Usuario { get; set; }

        public List<ReservaModel> ForKeyReserva_Usuario { get; set; }
    }
}