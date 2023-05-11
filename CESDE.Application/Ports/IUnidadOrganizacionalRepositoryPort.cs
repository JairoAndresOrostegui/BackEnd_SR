using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.UnidadOrganizacional;
using CESDE.Domain.Models;

namespace CESDE.Application.Ports
{
      public interface IUnidadOrganizacionalRepositoryPort
      {
            Task<UnidadOrganizacional> GetById(long id_unidad_organizacional);

            Task<bool> ValidateNameUnidadOrganizacional(string nombre_unidad_organizacional, long id_sede);

            Task<List<UnidadOrganizacionalDTO>> GetAll(long id_sede);

            Task<List<UnidadOrganizacionalDTO>> GetBySearch(string type, string search, long id_sede);

            Task<List<ComboDTO>> GetAllCombo();

            Task SaveUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional);

            Task UpdateUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional);

            Task DeleteUnidadOrganizacional(long id_unidad_organizacional);

            Task<List<ComboDTO>> GetByTipoEspacioCombo(long id_tipo_espacio);

            Task<UnidadOrganizacionalReservaDTO> GetByPadreAndTipoEspacio(ParametroReservaDTO parametroReservaDTO);

            //Task<UnidadOrganizacionalReservaDTO> GetByPadreAndTipoEspacio(ParametroReservaDTO parametroReservaDTO);
      }
}