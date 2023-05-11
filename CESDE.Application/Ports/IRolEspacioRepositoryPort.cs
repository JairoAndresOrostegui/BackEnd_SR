using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.Ports
{
      public interface IRolEspacioRepositoryPort
      {
            Task<List<ComboDTO>> GetAllByRolEspacio(long id_rol);
      }
}