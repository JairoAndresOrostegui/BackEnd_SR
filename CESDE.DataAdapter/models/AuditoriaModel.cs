using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CESDE.DataAdapter.models
{
    [Table("auditoria")]
    public class AuditoriaModel
    {
        [Key]
        [Column(TypeName = "int")]
        public long ID { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string accion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string tipo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string usuario { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime fecha { get; set; }
    }
}
