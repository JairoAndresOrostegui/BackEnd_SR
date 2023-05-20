namespace CESDE.Domain.Models
{
    public class Funcionalidad
    {
        public long id_funcionalidad { get; set; }
        public long id_componente { get; set; }
        public string nombre_funcionalidad { get; set; }
        public string url_funcionalidad { get; set; }
        public string estado_funcionalidad { get; set; }
    }
}
