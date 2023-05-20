namespace CESDE.Domain.Models
{
    public class PermisosRol
    {
        public long id_permisos_rol { get; set; }
        public long id_funcionalidad { get; set; }
        public string nombre_funcionalidad { get; set; }
        public long id_rol { get; set; }
        public string agregar { get; set; }
        public string modificar { get; set; }
        public string consultar { get; set; }
        public string eliminar { get; set; }
    }
}
