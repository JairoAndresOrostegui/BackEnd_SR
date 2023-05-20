using CESDE.Domain.Models;
using System.Collections.Generic;

namespace CESDE.Domain.DTO.Rol
{
    public class RolDTO
    {
        public long id_rol { get; set; }
        public string nombre_rol { get; set; }
        public int tamano_minimo_clave_rol { get; set; }
        public int vigencia_actualizacion_clave_rol { get; set; }
        public int maximo_autenticaciones_fallidas_rol { get; set; }
        public string actualizacion_datos_usuario_rol { get; set; }
        public string actualizacion_clave_usuario_rol { get; set; }
        public string estado_rol { get; set; }
        public int vigencia_actualizacion_datos_rol { get; set; }

        public List<PermisosRol> PermisosRol { get; set; }
    }
}
