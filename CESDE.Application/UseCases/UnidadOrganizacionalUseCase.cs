using CESDE.Application.Ports;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.UnidadOrganizacional;
using CESDE.Domain.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace CESDE.Application.UseCases
{
    public class UnidadOrganizacionalUseCase
    {
        private readonly IUnidadOrganizacionalRepositoryPort _unidadOrganizacionalRepository;

        public UnidadOrganizacionalUseCase(IUnidadOrganizacionalRepositoryPort unidadOrganizacionalRepository)
        {
            _unidadOrganizacionalRepository = unidadOrganizacionalRepository;
        }

        public async Task DeleteUnidadOrganizacional(long id_unidad_organizacional)
        {
            await _unidadOrganizacionalRepository.DeleteUnidadOrganizacional(id_unidad_organizacional);
        }

        public async Task<List<UnidadOrganizacionalDTO>> GetAll(long id_sede) => await _unidadOrganizacionalRepository.GetAll(id_sede);

        public async Task<List<UnidadOrganizacionalDTO>> GetBySearch(string type, string search, long id_sede) =>
              await _unidadOrganizacionalRepository.GetBySearch(type, search, id_sede);

        public async Task<List<ComboDTO>> GetAllCombo() =>
              await _unidadOrganizacionalRepository.GetAllCombo();

        public async Task<UnidadOrganizacional> GetById(long id_unidad_organizacional) =>
              await _unidadOrganizacionalRepository.GetById(id_unidad_organizacional);

        public async Task SaveUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional) =>
              await _unidadOrganizacionalRepository.SaveUnidadOrganizacional(unidadOrganizacional);

        public async Task UpdateUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional) =>
              await _unidadOrganizacionalRepository.UpdateUnidadOrganizacional(unidadOrganizacional);

        public async Task<bool> ValidateNameUnidadOrganizacional(string nombre_unidad_organizacional, long id_sede) =>
              await _unidadOrganizacionalRepository.ValidateNameUnidadOrganizacional(nombre_unidad_organizacional, id_sede);

        public async Task<List<ComboDTO>> GetByTipoEspacioCombo(long id_tipo_espacio) =>
              await _unidadOrganizacionalRepository.GetByTipoEspacioCombo(id_tipo_espacio);

        public async Task<UnidadOrganizacionalReservaDTO> GetByPadreAndTipoEspacio(ParametroReservaDTO parametroReservaDTO) =>
              await _unidadOrganizacionalRepository.GetByPadreAndTipoEspacio(parametroReservaDTO);
    }
}
