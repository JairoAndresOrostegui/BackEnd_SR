using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CESDE.Domain.Models;
using System.Collections.Generic;

namespace CESDE.DataAdapter.models
{
    [Table("rol")]
    public class RolModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_rol { get; set; }

        [Column(TypeName = "varchar(120)")]
        public string nombre_rol { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string estado_rol { get; set; }

        [Column(TypeName = "int")]
        public int nivel_rol { get; set; }

        public List<UsuarioModel> ForKeyUsuario_Rol { get; set; }
        public List<PermisosRolModel> ForKeyPermisos_Rol { get; set; }
        public List<RolEspacioModel> ForKeyRolEspacio_Rol { get; set; }

        public List<UnidadRolModel> ForKeyUnidadRol_Rol { get; set; }
    }
}