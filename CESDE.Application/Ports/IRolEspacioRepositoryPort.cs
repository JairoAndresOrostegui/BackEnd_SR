using CESDE.Domain.DTO.Combo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IRolEspacioRepositoryPort
    {
        Task<List<ComboDTO>> GetAllByRolEspacio(long id_rol);
    }
}