using System;

namespace CESDE.Domain.DTO.Usuario
{
      public class UsuarioDTO
      {
            public long id_usuario { get; set; }
            public string login_usuario { get; set; }
            public string primer_nombre_persona { get; set; }
            public string segundo_nombre_persona { get; set; }
            public string primer_apellido_persona { get; set; }
            public string segundo_apellido_persona { get; set; }
            public DateTime fecha_inicio_autenticacion_usuario { get; set; }
            public DateTime fecha_fin_autenticacion_usuario { get; set; }
            public int numero_intentos_autenticacion_usuario { get; set; }
            public string estado_clave_usuario { get; set; }
            public string estado_datos_usuario { get; set; }
            public string estado_usuario { get; set; }
            public string correo_institucional_usuario { get; set; }
            public string nombre_rol { get; set; }
            public string razon_social_persona { get; set; }

            public long id_rol { get; set; }
      }
}