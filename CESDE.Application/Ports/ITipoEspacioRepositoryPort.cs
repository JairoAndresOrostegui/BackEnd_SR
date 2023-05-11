using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;
using CESDE.Domain.Models;

namespace CESDE.Application.Ports
{
      public interface ITipoEspacioRepositoryPort
      {
            Task SaveTipoEspacio(TipoEspacio tipoEspacio);

            Task<TipoEspacio> GetById(long id_tipo_espacio);

            Task<bool> ValidateName(string nombre_tipo_espacio);

            Task<List<TipoEspacio>> GetAll();

            Task<List<TipoEspacio>> GetBySearch(string type, string search);

            Task<List<ComboDTO>> GetAllCombo();

            Task UpdateTipoEspacio(TipoEspacio tipoEspacio);

            Task DeleteTipoEspacio(long id_tipo_espacio);
      }
}