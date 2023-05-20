namespace CESDE.Domain.Models
{
    public class Municipio
    {
        public long municipio { get; set; }
        public long id_departamento { get; set; }
        public string nombre_municipio { get; set; }
        public string estado_municipio { get; set; }
    }
}