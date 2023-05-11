using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;

namespace CESDE.Application.UseCases
{
      public class MunicipioUseCase
      {
            private readonly IMunicipioRepositoryPort _municipioRepository;

            public MunicipioUseCase(IMunicipioRepositoryPort municipioRepository)
            {
                  _municipioRepository = municipioRepository;
            }

            public async Task<List<ComboDTO>> GetAllByDepartamento(long id_departamento) =>
                  await _municipioRepository.GetAllByDepartamento(id_departamento);

            public async Task<List<ComboDTO>> GetAll() => await _municipioRepository.GetAll();

            public async Task<long> GetByIdMunicipio(long id_municipio) => await _municipioRepository.GetByIdMunicipio(id_municipio);
      }
}