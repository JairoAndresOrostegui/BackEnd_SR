using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.Models;

namespace CESDE.Application.UseCases
{
      public class TipoEspacioUseCase
      {
            private readonly ITipoEspacioRepositoryPort _tipoEspacioRepository;

            public TipoEspacioUseCase(ITipoEspacioRepositoryPort tipoEspacioRepository)
            {
                  _tipoEspacioRepository = tipoEspacioRepository;
            }

            public async Task SaveTipoEspacio(TipoEspacio tipoEspacio) => await _tipoEspacioRepository.SaveTipoEspacio(tipoEspacio);

            public async Task<TipoEspacio> GetById(long id_tipo_espacio) => await _tipoEspacioRepository.GetById(id_tipo_espacio);

            public async Task<List<TipoEspacio>> GetAll() => await _tipoEspacioRepository.GetAll();

            public async Task<bool> ValidateName(string nombre_tipo_espacio) => await _tipoEspacioRepository.ValidateName(nombre_tipo_espacio);

            public async Task<List<TipoEspacio>> GetBySearch(string type, string search) => await _tipoEspacioRepository.GetBySearch(type, search);

            public async Task<List<ComboDTO>> GetAllCombo() => await _tipoEspacioRepository.GetAllCombo();

            public async Task UpdateTipoEspacio(TipoEspacio tipoEspacio) => await _tipoEspacioRepository.UpdateTipoEspacio(tipoEspacio);

            public async Task DeleteTipoEspacio(long id_tipo_espacio) => await _tipoEspacioRepository.DeleteTipoEspacio(id_tipo_espacio);
      }
}