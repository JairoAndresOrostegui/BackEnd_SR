using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.Models;

namespace CESDE.Application.UseCases
{
      public class CaracteristicaUseCase
      {
            private readonly ICaracteristicaRepositoryPort _caracteristicaRepository;

            public CaracteristicaUseCase(ICaracteristicaRepositoryPort caracteristicaRepository)
            {
                  _caracteristicaRepository = caracteristicaRepository;
            }

            public async Task SaveCaracteristica(Caracteristica caracteristica) => await _caracteristicaRepository.SaveCaracteristica(caracteristica);

            public async Task<Caracteristica> GetById(long id_caracteristica) => await _caracteristicaRepository.GetById(id_caracteristica);

            public async Task<bool> ValidateName(string nombre_tipo_espacio) => await _caracteristicaRepository.ValidateName(nombre_tipo_espacio);

            public async Task<List<Caracteristica>> GetAll() => await _caracteristicaRepository.GetAll();

            public async Task<List<Caracteristica>> GetBySearch(string type, string search) => await _caracteristicaRepository.GetBySearch(type, search);

            public async Task<List<ComboDTO>> GetAllCombo() => await _caracteristicaRepository.GetAllCombo();

            public async Task UpdateCaracteristica(Caracteristica caracteristica) => await _caracteristicaRepository.UpdateCaracteristica(caracteristica);

            public async Task DeleteCaracteristica(long id_caracteristica) => await _caracteristicaRepository.DeleteCaracteristica(id_caracteristica);
      }
}