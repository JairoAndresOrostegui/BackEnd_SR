using CESDE.Domain.DTO.Combo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IUnidadRolRepositoryPort
    {
        Task<List<ComboDTO>> GetAllByUnidadRol(long id_rol);
    }
}