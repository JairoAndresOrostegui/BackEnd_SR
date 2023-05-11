using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CESDE.DataAdapter.models
{
    [Table("persona")]
    public class PersonaModel
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public long id_persona { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string primer_nombre_persona { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string segundo_nombre_persona { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string primer_apellido_persona { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string segundo_apellido_persona { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string estado_persona { get; set; }


        public UsuarioModel ForKeyUsuario { get; set; }
    }
}