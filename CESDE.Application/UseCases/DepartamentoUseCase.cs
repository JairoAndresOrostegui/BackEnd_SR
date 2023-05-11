using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.UseCases
{
      public class DepartamentoUseCase
      {
            private readonly IDepartamentoRepositoryPort _departamentoRepository;

            public DepartamentoUseCase(IDepartamentoRepositoryPort departamentoRepository)
            {
                  _departamentoRepository = departamentoRepository;
            }

            public async Task<List<ComboDTO>> GetAllByPais(long id_pais) =>
                  await _departamentoRepository.GetAllByPais(id_pais);

            public async Task<List<ComboDTO>> GetAll() => await _departamentoRepository.GetAll();
      }
}