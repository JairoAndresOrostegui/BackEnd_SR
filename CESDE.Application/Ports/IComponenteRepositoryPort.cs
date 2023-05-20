using CESDE.Domain.DTO.Combo;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface IComponenteRepositoryPort
    {
        Task<List<ComboDTO>> GetAllCombo();
    }
}