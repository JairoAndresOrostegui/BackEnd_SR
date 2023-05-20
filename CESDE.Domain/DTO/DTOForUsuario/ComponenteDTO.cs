using System.Collections.Generic;

namespace CESDE.Domain.DTO.DTOForUsuario
{
    public class ComponenteDTO
    {
        public string nombre_componente { get; set; }

        public List<FuncionalidadDTO> funcionalidad { get; set; }
    }
}
