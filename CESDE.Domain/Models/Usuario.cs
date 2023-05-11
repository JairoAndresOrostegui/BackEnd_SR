using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.Models
{
    internal class Usuario
    {
        public long id_usuario{ get; set; }
        public long id_rol { get; set; }
        public long id_unidad_organizacional { get; set; }
        public string login_usuario { get; set; }
        public DateTime fecha_inicio_autenticacion_usuario { get; set; }
        public DateTime fecha_fin_autenticacion_usuario { get; set; }
        public DateTime fecha_actualizacion_clave_usuario { get; set; }
        public DateTime fecha_actualizacion_datos_usuario { get; set; }
        public int numero_intentos_autenticacion_usuario { get; set; }
        public string estado_datos_usuario { get; set; }
        public string estado_clave_usuario { get; set; }
        public string estado_usuario { get; set; }
        public string clave_usuario { get; set; }
        public string correo_institucional_usuario { get; set; }
        public long id_persona { get; set; }
    }
}
