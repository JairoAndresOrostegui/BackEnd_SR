﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESDE.Domain.Models
{
    internal class Persona
    {
        public long id_persona { get; set; }
        public string primer_nombre_persona { get; set; }
        public string segundo_nombre_persona { get; set; }
        public string primer_apellido_persona { get; set; }
        public string segundo_apellido_persona { get; set; }
        public string estado_persona { get; set; }
    }
}
