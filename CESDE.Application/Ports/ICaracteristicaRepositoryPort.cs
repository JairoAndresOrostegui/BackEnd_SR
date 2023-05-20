using CESDE.Domain.DTO.Combo;
using CESDE.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.Ports
{
    public interface ICaracteristicaRepositoryPort
    {
        Task SaveCaracteristica(Caracteristica caracteristica);

        Task<Caracteristica> GetById(long id_caracteristica);

        Task<bool> ValidateName(string nombre_tipo_espacio);

        Task<List<Caracteristica>> GetAll();

        Task<List<Caracteristica>> GetBySearch(string type, string search);

        Task<List<ComboDTO>> GetAllCombo();

        Task UpdateCaracteristica(Caracteristica caracteristica);

        Task DeleteCaracteristica(long id_caracteristica);
    }
}