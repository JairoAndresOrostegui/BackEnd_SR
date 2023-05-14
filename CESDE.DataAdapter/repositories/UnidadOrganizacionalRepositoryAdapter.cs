using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.DataAdapter.models;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.UnidadOrganizacional;
using CESDE.Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace CESDE.DataAdapter.repositories
{
      public class UnidadOrganizacionalRepositoryAdapter : IUnidadOrganizacionalRepositoryPort
      {

            private readonly CESDE_Context _context;

            public UnidadOrganizacionalRepositoryAdapter(CESDE_Context context)
            {
                  _context = context;
            }

            public async Task DeleteUnidadOrganizacional(long id_unidad_organizacional)
            {
                  var validate = await _context.UnidadOrganizacionalModels.AnyAsync(sear => sear.id_unidad_organizacional == id_unidad_organizacional);
                  if (!validate)
                        throw new Exception(Enums.MessageDoesNotExist);

                  //var usuario = await _context.UsuarioModels.AnyAsync(vali => vali.id_unidad_organizacional == id_unidad_organizacional);
                  //if (usuario)
                  //      throw new Exception(Enums.MessageReference);

                  //FALTA VALIDAR CON RESERVA SI TIENE UNA UNIDAD ORGANIZACIONAL

                  //ELIMINAR LAS CARACTERISTICAS DE LA UNIDAD
                  var idUnidad_Caracteristica = await _context.UnidadOrganizacionalCaracteristicaModels
                        .Where(rol => rol.id_unidad_organizacional == id_unidad_organizacional)
                        .Select(id => id.id_unidad_organizacional_caracteristica).ToListAsync();

                  foreach (var unidad_caracteristicas in idUnidad_Caracteristica)
                  {
                        _context.UnidadOrganizacionalCaracteristicaModels
                              .Remove(new UnidadOrganizacionalCaracteristicaModel()
                              { id_unidad_organizacional_caracteristica = unidad_caracteristicas });
                  }
                  await _context.SaveChangesAsync();

                  //ELIMINAR LA UNIDAD ORGANIZACIONAL
                  _context.UnidadOrganizacionalModels
                        .Remove(new UnidadOrganizacionalModel() { id_unidad_organizacional = id_unidad_organizacional });
                  await _context.SaveChangesAsync();
            }

            public async Task<List<UnidadOrganizacionalDTO>> GetAll(long id_sede)
            {
                  var entidades = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                        .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad).Include(muni => muni.ForKeyMunicipio_Unidad)
                        .Include(uoc => uoc.ForKeyUOC_UnidadOrgani).Where(x => x.id_unidad_organizacional_padre == id_sede)
                        .Select(enti => new UnidadOrganizacionalDTO()
                        {
                              id_unidad_organizacional = enti.id_unidad_organizacional,
                              nombre_unidad_organizacional = enti.nombre_unidad_organizacional.UpperFirstChar(),
                              nombre_tipo_espacio = enti.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                              nombre_municipio = enti.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                              nombre_departamento = enti.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                              piso_unidad_organizacional = enti.piso_unidad_organizacional,
                              capacidad_unidad_organizacional = enti.capacidad_unidad_organizacional,
                              nombre_unidad_organizacional_padre = enti.id_unidad_organizacional_padre.ToString(),
                              estado_unidad_organizacional = enti.estado_unidad_organizacional.UpperFirstChar(),
                              caracteristicas = enti.ForKeyUOC_UnidadOrgani.OrderBy(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica)
                              .Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                              {
                                    id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                    nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                    nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                    cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                              }).ToList(),
                        }).ToListAsync();

                  if (entidades.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return entidades;
            }

            public async Task<List<ComboDTO>> GetAllCombo()
            {
                  return await _context.UnidadOrganizacionalModels
                        .OrderBy(x => x.nombre_unidad_organizacional)
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_unidad_organizacional,
                              label = enti.nombre_unidad_organizacional.ToUpper()
                        }).ToListAsync();
            }

            public async Task<UnidadOrganizacional> GetById(long id_unidad_organizacional)
            {
                  var entidadMap = await _context.UnidadOrganizacionalModels
                        .Where(unid => unid.id_unidad_organizacional == id_unidad_organizacional)
                        .Select(enti => new UnidadOrganizacional()
                        {
                              id_unidad_organizacional = enti.id_unidad_organizacional,
                              id_tipo_espacio = enti.id_tipo_espacio,
                              id_municipio = enti.id_municipio,
                              nombre_unidad_organizacional = enti.nombre_unidad_organizacional.UpperFirstChar(),
                              piso_unidad_organizacional = enti.piso_unidad_organizacional,
                              capacidad_unidad_organizacional = enti.capacidad_unidad_organizacional,
                              estado_unidad_organizacional = enti.estado_unidad_organizacional.ToLower(),
                              id_unidad_organizacional_padre = enti.id_unidad_organizacional_padre,
                              caracteristicas = enti.ForKeyUOC_UnidadOrgani.Select(map => new UnidadOrganizacionalCaracteristica()
                              {
                                    id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                    id_caracteristica = map.id_caracteristica,
                                    nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                    id_unidad_organizacional = map.id_unidad_organizacional,
                                    cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                              }).ToList(),
                        }).FirstOrDefaultAsync();

                  if (entidadMap == null)
                        throw new Exception(Enums.MessageDoesNotExist);

                  return entidadMap;
            }

            public async Task<List<UnidadOrganizacionalDTO>> GetBySearch(string type, string search, long id_sede)
            {
                  search = search.ToUpper();
                  List<UnidadOrganizacionalDTO> listUnidadOrganizacional = new List<UnidadOrganizacionalDTO>();

                  if (type == "nombre")
                  {
                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad).Include(muni => muni.ForKeyMunicipio_Unidad)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              sear.nombre_unidad_organizacional.ToUpper().Contains(search))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.OrderBy(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica)
                                    .Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (type == "tipo")
                  {
                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad).Include(muni => muni.ForKeyMunicipio_Unidad)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              sear.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.ToUpper().Contains(search))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.OrderBy(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica)
                                    .Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (type == "municipio")
                  {
                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad).Include(muni => muni.ForKeyMunicipio_Unidad)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              sear.ForKeyMunicipio_Unidad.nombre_municipio.ToUpper().Contains(search))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.OrderBy(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica)
                                    .Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (type == "capacidad")
                  {
                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad).Include(muni => muni.ForKeyMunicipio_Unidad)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              sear.capacidad_unidad_organizacional >= Convert.ToInt32(search))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.OrderBy(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica)
                                    .Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (type == "estado")
                  {
                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad)
                              .Include(muni => muni.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani).Include(depart => depart.ForKeyMunicipio_Unidad)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              sear.estado_unidad_organizacional.ToUpper().Equals(search))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (type == "caracteristica")
                  {
                        var lsIdUnidad = await _context.UnidadOrganizacionalCaracteristicaModels
                              .Include(x => x.ForKeyCaracteristica_UOC)
                              .Include(x => x.ForKeyUnidadOrgani_UOC).Where(x => x.ForKeyCaracteristica_UOC.nombre_caracteristica.Contains(search))
                              .Select(x => x.id_unidad_organizacional).ToListAsync();

                        listUnidadOrganizacional = await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                              .Include(tiEspa => tiEspa.ForKeyTipoEspacioUnidad)
                              .Include(muni => muni.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni)
                              .Include(uoc => uoc.ForKeyUOC_UnidadOrgani).Include(depart => depart.ForKeyMunicipio_Unidad)
                              .Where(sear => sear.id_unidad_organizacional_padre == id_sede &&
                              lsIdUnidad.Contains(sear.id_unidad_organizacional))
                              .Select(unidad => new UnidadOrganizacionalDTO()
                              {
                                    id_unidad_organizacional = unidad.id_unidad_organizacional,
                                    nombre_unidad_organizacional = unidad.nombre_unidad_organizacional.UpperFirstChar(),
                                    nombre_tipo_espacio = unidad.ForKeyTipoEspacioUnidad.nombre_tipo_espacio.UpperFirstChar(),
                                    nombre_municipio = unidad.ForKeyMunicipio_Unidad.nombre_municipio.UpperFirstChar(),
                                    nombre_departamento = unidad.ForKeyMunicipio_Unidad.ForKeyDepartamentoMuni.nombre_departamento.UpperFirstChar(),
                                    piso_unidad_organizacional = unidad.piso_unidad_organizacional,
                                    capacidad_unidad_organizacional = unidad.capacidad_unidad_organizacional,
                                    nombre_unidad_organizacional_padre = unidad.id_unidad_organizacional_padre.ToString(),
                                    estado_unidad_organizacional = unidad.estado_unidad_organizacional.UpperFirstChar(),
                                    caracteristicas = unidad.ForKeyUOC_UnidadOrgani.Select(map => new UnidadOrganizacionalCaracteristicaDTO()
                                    {
                                          id_unidad_organizacional_caracteristica = map.id_unidad_organizacional_caracteristica,
                                          nombre_caracteristica = map.ForKeyCaracteristica_UOC.nombre_caracteristica.UpperFirstChar(),
                                          nombre_unidad_organizacional = map.ForKeyUnidadOrgani_UOC.nombre_unidad_organizacional.UpperFirstChar(),
                                          cantidad_unidad_organizacional_caracteristica = map.cantidad_unidad_organizacional_caracteristica
                                    }).ToList(),
                              }).ToListAsync();
                  }

                  if (listUnidadOrganizacional.Count() == 0)
                        throw new Exception(Enums.MessageNoRecord);

                  return listUnidadOrganizacional;
            }

            public async Task SaveUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional)
            {
                  //var validate = await _context.UnidadOrganizacionalModels
                  //      .AnyAsync(sear => sear.nombre_unidad_organizacional.ToUpper()
                  //      .Equals(unidadOrganizacional.nombre_unidad_organizacional.ToUpper()));
                  //if (validate)
                  //      throw new Exception(Enums.MessageExist);

                  var entidadMap = new UnidadOrganizacionalModel()
                  {
                        id_unidad_organizacional = unidadOrganizacional.id_unidad_organizacional,
                        id_tipo_espacio = unidadOrganizacional.id_tipo_espacio,
                        id_municipio = unidadOrganizacional.id_municipio,
                        nombre_unidad_organizacional = unidadOrganizacional.nombre_unidad_organizacional.ToLower(),
                        piso_unidad_organizacional = unidadOrganizacional.piso_unidad_organizacional,
                        capacidad_unidad_organizacional = unidadOrganizacional.capacidad_unidad_organizacional,
                        estado_unidad_organizacional = unidadOrganizacional.estado_unidad_organizacional.ToLower(),
                        id_unidad_organizacional_padre = unidadOrganizacional.id_unidad_organizacional_padre,
                  };

                  _context.Add(entidadMap);
                  await _context.SaveChangesAsync();

                  foreach (var caracteristica in unidadOrganizacional.caracteristicas)
                  {
                        UnidadOrganizacionalCaracteristicaModel unidadOrganizacionalCaracteristica = new UnidadOrganizacionalCaracteristicaModel()
                        {
                              id_unidad_organizacional_caracteristica = caracteristica.id_unidad_organizacional_caracteristica,
                              id_caracteristica = caracteristica.id_caracteristica,
                              id_unidad_organizacional = entidadMap.id_unidad_organizacional,
                              cantidad_unidad_organizacional_caracteristica = caracteristica.cantidad_unidad_organizacional_caracteristica
                        };

                        _context.UnidadOrganizacionalCaracteristicaModels.Add(unidadOrganizacionalCaracteristica);
                  }

                  await _context.SaveChangesAsync();
            }

            public async Task UpdateUnidadOrganizacional(UnidadOrganizacional unidadOrganizacional)
            {
                  var validate = await _context.UnidadOrganizacionalModels
                        .AnyAsync(sear => sear.id_unidad_organizacional == unidadOrganizacional.id_unidad_organizacional);
                  if (!validate)
                        throw new Exception(Enums.MessageDoesNotExist);

                  var entidadMap = new UnidadOrganizacionalModel()
                  {
                        id_unidad_organizacional = unidadOrganizacional.id_unidad_organizacional,
                        id_tipo_espacio = unidadOrganizacional.id_tipo_espacio,
                        id_municipio = unidadOrganizacional.id_municipio,
                        nombre_unidad_organizacional = unidadOrganizacional.nombre_unidad_organizacional.ToLower(),
                        piso_unidad_organizacional = unidadOrganizacional.piso_unidad_organizacional,
                        capacidad_unidad_organizacional = unidadOrganizacional.capacidad_unidad_organizacional,
                        estado_unidad_organizacional = unidadOrganizacional.estado_unidad_organizacional.ToLower(),
                        id_unidad_organizacional_padre = unidadOrganizacional.id_unidad_organizacional_padre,
                  };

                  _context.Update(entidadMap);
                  await _context.SaveChangesAsync();

                  //ELIMINAR LAS CARACTERISTICAS DE LA UNIDAD ORGANIZACIONAL
                  var idUnidad_Caracteristica = await _context.UnidadOrganizacionalCaracteristicaModels
                        .Where(sear => sear.id_unidad_organizacional == unidadOrganizacional.id_unidad_organizacional)
                        .Select(id => id.id_unidad_organizacional_caracteristica).ToListAsync();

                  foreach (var unidad_caracteristicas in idUnidad_Caracteristica)
                  {
                        _context.UnidadOrganizacionalCaracteristicaModels
                              .Remove(new UnidadOrganizacionalCaracteristicaModel()
                              { id_unidad_organizacional_caracteristica = unidad_caracteristicas });
                  }
                  await _context.SaveChangesAsync();

                  ////INSERTA NUEVAMENTE LAS CARACTERISTICAS CON SU UNIDAD ORGANIZACIONAL
                  foreach (var caracteristica in unidadOrganizacional.caracteristicas)
                  {
                        UnidadOrganizacionalCaracteristicaModel unidadOrganizacionalCaracteristica = new UnidadOrganizacionalCaracteristicaModel()
                        {
                              id_unidad_organizacional_caracteristica = caracteristica.id_unidad_organizacional_caracteristica,
                              id_caracteristica = caracteristica.id_caracteristica,
                              id_unidad_organizacional = entidadMap.id_unidad_organizacional,
                              cantidad_unidad_organizacional_caracteristica = caracteristica.cantidad_unidad_organizacional_caracteristica,
                        };

                        _context.UnidadOrganizacionalCaracteristicaModels.Add(unidadOrganizacionalCaracteristica);
                  }

                  await _context.SaveChangesAsync();
            }

            public async Task<bool> ValidateNameUnidadOrganizacional(string nombre_unidad_organizacional, long id_sede)
            {
                  var lsData = await _context.UnidadOrganizacionalModels
                        .Where(unid => unid.id_unidad_organizacional_padre == id_sede).ToListAsync();

                  bool validateExistence = lsData.Any(x => x.nombre_unidad_organizacional.ToUpper().Equals(
                        nombre_unidad_organizacional.ToUpper()));

                  return validateExistence;
            }

            public async Task<List<ComboDTO>> GetByTipoEspacioCombo(long id_tipo_espacio)
            {
                  return await _context.UnidadOrganizacionalModels.OrderBy(x => x.nombre_unidad_organizacional)
                        .Where(sear => sear.id_tipo_espacio == id_tipo_espacio)
                        .Select(enti => new ComboDTO()
                        {
                              value = enti.id_unidad_organizacional,
                              label = enti.nombre_unidad_organizacional.ToUpper()
                        }).OrderBy(ord => ord.value).ToListAsync();
            }

            public async Task<UnidadOrganizacionalReservaDTO> GetByPadreAndTipoEspacio(ParametroReservaDTO parametroReservaDTO)
            {
                  int contador = 0;
                  int contadorReservarDisponibles = 0;

                  List<ComboDTO> listDisponibles = new List<ComboDTO>();
                  List<ComboDTO> listReservadas = new List<ComboDTO>();

                  List<ConsultaReservaDTO> lsReservasReservadas = new List<ConsultaReservaDTO>();
                  List<long> lsIdUnidadesFiltradas = new List<long>();
                  List<long> lsIdUnidadesDisponibles = new List<long>();
                  List<long> lsIdUnidadesReservadas = new List<long>();
                  List<ComboReservas> lsUnidadOrganizacionalDisponible = new List<ComboReservas>();
                  List<ComboReservas> lsUnidadOrganizacionalReservada = new List<ComboReservas>();

                  //Buscar por id padre y id tipo espacio
                  if (parametroReservaDTO.id_caracteristica == 0)
                  {
                        lsIdUnidadesFiltradas = await _context.UnidadOrganizacionalModels.Include(tipo => tipo.ForKeyTipoEspacioUnidad)

                        .Where(sear => sear.id_unidad_organizacional_padre == parametroReservaDTO.id_unidad_organizacional_padre &&
                              sear.id_tipo_espacio == parametroReservaDTO.id_tipo_espacio &&
                              sear.capacidad_unidad_organizacional >= parametroReservaDTO.capacidad_unidad_organizacional)

                        .Select(idResult => idResult.id_unidad_organizacional).ToListAsync();
                  }
                  else
                  {
                        lsIdUnidadesFiltradas = await _context.UnidadOrganizacionalModels.Include(tipo => tipo.ForKeyTipoEspacioUnidad)

                        .Where(sear => sear.id_unidad_organizacional_padre == parametroReservaDTO.id_unidad_organizacional_padre &&
                              sear.id_tipo_espacio == parametroReservaDTO.id_tipo_espacio &&
                              sear.capacidad_unidad_organizacional >= parametroReservaDTO.capacidad_unidad_organizacional &&
                              sear.ForKeyUOC_UnidadOrgani.Select(x => x.id_caracteristica == parametroReservaDTO.id_caracteristica).FirstOrDefault())

                        .Select(idResult => idResult.id_unidad_organizacional).ToListAsync();
                  }


                  //Buscar todas las reservas por id unidad
                  foreach (var ids in lsIdUnidadesFiltradas)
                  {
                        var lsReservaReservados = await _context.ReservaModels
                              .Where(reser => reser.id_unidad_organizacional == ids)
                              .Select(entid => new ConsultaReservaDTO()
                              {
                                    id_unidad_organizacional = entid.id_unidad_organizacional,
                                    fecha_inicio_reserva = entid.fecha_inicio_reserva,
                                    fecha_fin_reserva = entid.fecha_fin_reserva,
                                    reservaDias = entid.ForKeyReservaDia_Reserva.Select(res => new ReservaDia()
                                    {
                                          reserva_dia_id = res.reserva_dia_id,
                                          id_reserva = res.id_reserva,
                                          reserva_dia_dia = res.reserva_dia_dia,
                                          reserva_dia_hora_inicio = res.reserva_dia_hora_inicio,
                                          //reserva_dia_hora_fin = res.reserva_dia_hora_fin
                                    }).ToList()
                              }).ToListAsync();

                        foreach (var item in lsReservaReservados)
                        {
                              lsReservasReservadas.Add(item);
                        }
                  }

                  //VERIFICAR LAS UNIDADES QUE ESTAN Y NO ESTAN DISPONIBLES
                  foreach (var lsId_UnidadOrganizacional in lsIdUnidadesFiltradas)
                  {
                        contadorReservarDisponibles = 0;

                        foreach (var item in lsReservasReservadas)
                        {
                              if (lsId_UnidadOrganizacional == item.id_unidad_organizacional)
                              {
                                    contadorReservarDisponibles += 1;
                              }
                        }

                        if (contadorReservarDisponibles == 0)
                        {
                              lsIdUnidadesDisponibles.Add(lsId_UnidadOrganizacional);
                        }
                  }

                  //CODIGO QUE FUE COMENTAREADO
                  if (lsReservasReservadas.Count() != 0)
                  {
                        foreach (var lsReservaExiste in lsReservasReservadas)
                        {
                              contador = 0;

                              if (parametroReservaDTO.fecha_fin_reserva <= lsReservaExiste.fecha_inicio_reserva)
                              {

                              }
                              else if (parametroReservaDTO.fecha_inicio_reserva >= lsReservaExiste.fecha_fin_reserva)
                              {

                              }
                              else if (parametroReservaDTO.fecha_inicio_reserva >= lsReservaExiste.fecha_inicio_reserva &&
                                    parametroReservaDTO.fecha_inicio_reserva <= lsReservaExiste.fecha_fin_reserva)
                              {
                                    foreach (var lsDias in lsReservaExiste.reservaDias)
                                    {
                                          if (parametroReservaDTO.reserva_dia_dia.Any(x => lsDias.reserva_dia_dia.Contains(x.ToString())))
                                          {
                                                var formatHoraInicioData = DateTime.Parse(lsDias.reserva_dia_hora_inicio.ToString());
                                                var formatHoraFinData = DateTime.Parse(lsDias.reserva_dia_hora_fin.ToString());

                                                var formatHoraInicio = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_inicio);
                                                var formatHoraFin = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_fin);

                                                if (formatHoraFin == formatHoraInicioData)
                                                {

                                                }
                                                else if (formatHoraFinData == formatHoraInicio)
                                                {

                                                }
                                                else if (formatHoraInicio >= formatHoraInicioData && formatHoraInicio <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraFin >= formatHoraInicioData && formatHoraFin <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraInicio < formatHoraInicioData && formatHoraFin > formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                          }
                                          else
                                          {
                                                contador += 1;
                                          }
                                    }
                              }
                              else if (parametroReservaDTO.fecha_fin_reserva >= lsReservaExiste.fecha_inicio_reserva && parametroReservaDTO.fecha_fin_reserva <= lsReservaExiste.fecha_fin_reserva)
                              {
                                    foreach (var lsDias in lsReservaExiste.reservaDias)
                                    {
                                          if (parametroReservaDTO.reserva_dia_dia.Any(x => lsDias.reserva_dia_dia.Contains(x.ToString())))
                                          {
                                                var formatHoraInicioData = DateTime.Parse(lsDias.reserva_dia_hora_inicio.ToString());
                                                var formatHoraFinData = DateTime.Parse(lsDias.reserva_dia_hora_fin.ToString());

                                                var formatHoraInicio = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_inicio);
                                                var formatHoraFin = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_fin);

                                                if (formatHoraFin == formatHoraInicioData)
                                                {

                                                }
                                                else if (formatHoraFinData == formatHoraInicio)
                                                {

                                                }
                                                else if (formatHoraInicio >= formatHoraInicioData && formatHoraInicio <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraFin >= formatHoraInicioData && formatHoraFin <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraInicio < formatHoraInicioData && formatHoraFin > formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                          }
                                          else
                                          {
                                                contador += 1;
                                          }
                                    }
                              }
                              else if (parametroReservaDTO.fecha_inicio_reserva < lsReservaExiste.fecha_inicio_reserva && parametroReservaDTO.fecha_fin_reserva > lsReservaExiste.fecha_fin_reserva)
                              {
                                    foreach (var lsDias in lsReservaExiste.reservaDias)
                                    {
                                          if (parametroReservaDTO.reserva_dia_dia.Any(x => lsDias.reserva_dia_dia.Contains(x.ToString())))
                                          {
                                                var formatHoraInicioData = DateTime.Parse(lsDias.reserva_dia_hora_inicio.ToString());
                                                var formatHoraFinData = DateTime.Parse(lsDias.reserva_dia_hora_fin);

                                                var formatHoraInicio = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_inicio);
                                                var formatHoraFin = DateTime.Parse(parametroReservaDTO.reserva_dia_hora_fin);

                                                if (formatHoraFin == formatHoraInicioData)
                                                {

                                                }
                                                else if (formatHoraFinData == formatHoraInicio)
                                                {

                                                }
                                                else if (formatHoraInicio >= formatHoraInicioData && formatHoraInicio <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraFin >= formatHoraInicioData && formatHoraFin <= formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                                else if (formatHoraInicio < formatHoraInicioData && formatHoraFin > formatHoraFinData)
                                                {
                                                      contador += 1;
                                                }
                                          }
                                          else
                                          {
                                                contador += 1;
                                          }
                                    }
                              }

                              if (contador == 0)
                              {
                                    if (lsIdUnidadesDisponibles.Any(x => lsReservaExiste.id_unidad_organizacional == x))
                                    {

                                    }
                                    else
                                    {
                                          lsIdUnidadesDisponibles.Add(lsReservaExiste.id_unidad_organizacional);
                                    }
                              }
                              else
                              {
                                    if (lsIdUnidadesReservadas.Any(x => lsReservaExiste.id_unidad_organizacional == x))
                                    {

                                    }
                                    else
                                    {
                                          lsIdUnidadesReservadas.Add(lsReservaExiste.id_unidad_organizacional);
                                    }
                              }
                        }
                  }

                  //BUSQUEDA DE UNIDADES DISPONIBLES
                  foreach (var item in lsIdUnidadesDisponibles)
                  {
                        var lsUnidadesDisponibles = await _context.UnidadOrganizacionalModels.Include(tipo => tipo.ForKeyTipoEspacioUnidad)
                              .Where(sear => sear.id_unidad_organizacional == item)
                              .Select(enti => new ComboReservas()
                              {
                                    id_unidad_organizacional = enti.id_unidad_organizacional,
                                    nombre_unidad_organizacional = enti.nombre_unidad_organizacional.UpperFirstChar()
                              }).FirstOrDefaultAsync();

                        listDisponibles.Add(new ComboDTO()
                        {
                              value = lsUnidadesDisponibles.id_unidad_organizacional,
                              label = lsUnidadesDisponibles.nombre_unidad_organizacional.ToUpper()
                        });

                        lsUnidadOrganizacionalDisponible.Add(lsUnidadesDisponibles);
                  }

                  //BUSQUEDA DE UNIDADES RESERVADAS
                  foreach (var item in lsIdUnidadesReservadas)
                  {
                        var lsUnidadesDisponibles = await _context.UnidadOrganizacionalModels.Include(tipo => tipo.ForKeyTipoEspacioUnidad)
                              .Where(sear => sear.id_unidad_organizacional == item)
                              .Select(enti => new ComboReservas()
                              {
                                    id_unidad_organizacional = enti.id_unidad_organizacional,
                                    nombre_unidad_organizacional = enti.nombre_unidad_organizacional.UpperFirstChar()
                              }).FirstOrDefaultAsync();

                        listReservadas.Add(new ComboDTO()
                        {
                              value = lsUnidadesDisponibles.id_unidad_organizacional,
                              label = lsUnidadesDisponibles.nombre_unidad_organizacional.ToUpper()
                        });

                        lsUnidadOrganizacionalReservada.Add(lsUnidadesDisponibles);
                  }

                  var Result = new UnidadOrganizacionalReservaDTO()
                  {
                        reservas = parametroReservaDTO.estado == "disponible" ? lsUnidadOrganizacionalDisponible : lsUnidadOrganizacionalReservada,
                        ReservaDisponible = listDisponibles,
                        ReservaReservada = listReservadas
                  };

                  return Result;
            }
      }
}