using System.Collections.Generic;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.DTOForUsuario;

namespace CESDE.Domain.DTO.Usuario
{
    public class SistemaAutenticacionDTO
    {
        public long id_usuario { get; set; }
        public long id_persona { get; set; }
        public string primer_nombre_persona { get; set; }
        public string primer_apellido_persona { get; set; }
        public long id_rol { get; set; }
        public string nombre_rol { get; set; }
        public long id_unidad_organizacional { get; set; }
        public string nombre_unidad_organizacional { get; set; }

        public int nivel_rol { get; set; }
        public List<ComboDTO> rol_espacio { get; set; }
        public List<ComboDTO> unidad_rol { get; set; }

        public List<ComponenteDTO> componente { get; set; }
    }
}