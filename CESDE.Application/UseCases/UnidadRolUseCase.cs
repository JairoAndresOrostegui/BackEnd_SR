using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.UseCases
{
      public class UnidadRolUseCase
      {
            private readonly IUnidadRolRepositoryPort _unidadRolRepository;

            public UnidadRolUseCase(IUnidadRolRepositoryPort unidadRolRepository)
            {
                  _unidadRolRepository = unidadRolRepository;
            }

            public async Task<List<ComboDTO>> GetAllByUnidadRol(long id_rol) =>
                  await _unidadRolRepository.GetAllByUnidadRol(id_rol);
      }
}