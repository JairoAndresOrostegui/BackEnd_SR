using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.UseCases
{
      public class RolEspacioUseCase
      {
            private readonly IRolEspacioRepositoryPort _rolEspacioRepository;

            public RolEspacioUseCase(IRolEspacioRepositoryPort rolEspacioRepository)
            {
                  _rolEspacioRepository = rolEspacioRepository;
            }

            public async Task<List<ComboDTO>> GetAllByRolEspacio(long id_rol) =>
                  await _rolEspacioRepository.GetAllByRolEspacio(id_rol);
      }
}