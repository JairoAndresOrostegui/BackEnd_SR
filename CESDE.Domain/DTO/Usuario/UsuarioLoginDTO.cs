namespace CESDE.Domain.DTO.Usuario
{
    public class UsuarioLoginDTO
    {
        public string area_rol { get; set; }

        public long id_usuario { get; set; }
        public string login_usuario { get; set; }
        public long id_persona { get; set; }
        public string primer_nombre_persona { get; set; }
        public string primer_apellido_persona { get; set; }
        public long id_unidad_organizacional { get; set; }
        public string nombre_unidad_organizacional { get; set; }
        public string nombre_rol { get; set; }
        public long id_rol { get; set; }
        public int nivel_rol { get; set; }
    }
}