﻿using CESDE.Application.Ports;
using CESDE.DataAdapter.helpers;
using CESDE.DataAdapter.models;
using CESDE.Domain.DTO.Combo;
using CESDE.Domain.DTO.Reserva;
using CESDE.Domain.DTO.Reserva.Filtros;
using CESDE.Domain.DTO.Reserva.Informe;
using CESDE.Domain.DTO.Reserva.Reporte;
using CESDE.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CESDE.DataAdapter.repositories
{
    public class ReservaRepositoryAdapter : IReservaRepositoryPort
    {

        private readonly CESDE_Context _context;

        public ReservaRepositoryAdapter(CESDE_Context context)
        {
            _context = context;
        }

        public async Task DeleteReserva(long id_reserva)
        {
            var validate = await _context.ReservaModels.AnyAsync(sear => sear.id_reserva == id_reserva);
            if (!validate)
                throw new Exception(Enums.MessageDoesNotExist);

            var idReservaDia = await _context.ReservaDiaModels.Where(sear => sear.id_reserva == id_reserva)
                  .Select(id => id.reserva_dia_id).ToListAsync();

            foreach (var reser in idReservaDia)
                _context.ReservaDiaModels.Remove(new ReservaDiaModel() { reserva_dia_id = reser });

            await _context.SaveChangesAsync();

            _context.ReservaModels.Remove(new ReservaModel() { id_reserva = id_reserva });
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReservaDTO>> GetAll()
        {
            var entidades = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
                  .Select(enti => new ReservaDTO()
                  {
                      id_reserva = enti.id_reserva,
                      nombre_unidad_organizacional = enti.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional.UpperFirstChar(),
                      nombre_submodulo = enti.nombre_grupo,
                      fecha_inicio_reserva = enti.fecha_inicio_reserva,
                      fecha_fin_reserva = enti.fecha_fin_reserva,
                      nombre_usuario_colaborador = enti.nombre_usuario_colaborador,
                      descripcion_reserva = enti.descripcion_reserva.UpperFirstChar(),
                      estado_reserva = enti.estado_reserva.UpperFirstChar(),
                      reservaDias = enti.ForKeyReservaDia_Reserva.Select(reser => new ReservaDiaDTO()
                      {
                          reserva_dia_id = reser.reserva_dia_id,
                          id_reserva = reser.id_reserva,
                          reserva_dia_dia = reser.reserva_dia_dia.UpperFirstChar(),
                          reserva_dia_hora_inicio = reser.reserva_dia_hora_inicio,
                          jornada = reser.jornada
                          //reserva_dia_hora_fin = reser.reserva_dia_hora_fin
                      }).ToList()
                  }).ToListAsync();

            if (entidades.Count() == 0)
                throw new Exception(Enums.MessageNoRecord);

            return entidades;
        }

        public async Task<Reserva> GetById(long id_reserva)
        {
            var validate = await _context.ReservaModels.AnyAsync(sear => sear.id_reserva == id_reserva);

            if (!validate)
                throw new Exception(Enums.MessageDoesNotExist);

            var entidadMap = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
                  .Where(user => user.id_reserva == id_reserva).Select(enti => new Reserva()
                  {
                      id_reserva = enti.id_reserva,
                      id_unidad_organizacional = enti.id_unidad_organizacional,
                      identificador_grupo = enti.identificador_grupo,
                      nombre_grupo = enti.nombre_grupo.ToLower(),
                      id_usuario_reserva = enti.id_usuario_reserva,
                      fecha_inicio_reserva = enti.fecha_inicio_reserva,
                      fecha_fin_reserva = enti.fecha_fin_reserva,
                      descripcion_reserva = enti.descripcion_reserva,
                      estado_reserva = enti.estado_reserva.ToLower(),
                      id_usuario_colaborador = enti.id_usuario_colaborador,
                      nombre_usuario_colaborador = enti.nombre_usuario_colaborador.ToLower(),
                      nivel = enti.nivel,
                      codigo_programa = enti.codigo_programa,
                      nombre_programa = enti.nombre_programa.ToLower(),
                      //id_rol = enti.id_rol,
                      submodulo = enti.submodulo,
                      reservaDias = enti.ForKeyReservaDia_Reserva.Select(reser => new ReservaDia()
                      {
                          reserva_dia_id = reser.reserva_dia_id,
                          id_reserva = reser.id_reserva,
                          reserva_dia_dia = reser.reserva_dia_dia.UpperFirstChar(),
                          reserva_dia_hora_inicio = reser.reserva_dia_hora_inicio,
                          //reserva_dia_hora_fin = reser.reserva_dia_hora_fin
                      }).ToList()
                  }).FirstOrDefaultAsync();
            return entidadMap;
        }

        public async Task<List<ReservaDTO>> GetBySearch(string type, string search)
        {
            List<ReservaModel> lsReservaModel = new List<ReservaModel>();
            List<ReservaDTO> lsReserva = new List<ReservaDTO>();

            if (type == "unidad_organizacional")
            {
                lsReservaModel = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
                      .Include(diaReser => diaReser.ForKeyReservaDia_Reserva)
                      .Where(sear => sear.id_unidad_organizacional == Convert.ToInt64(search)).ToListAsync();
            }

            if (type == "programa")
            {
                lsReservaModel = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
                      .Include(diaReser => diaReser.ForKeyReservaDia_Reserva)
                      .Where(sear => sear.id_unidad_organizacional == Convert.ToInt64(search)).ToListAsync();
            }

            if (type == "submodulo")
            {
                lsReservaModel = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
                      .Include(diaReser => diaReser.ForKeyReservaDia_Reserva)
                      .Where(x => x.nombre_grupo.ToUpper().Contains(search.ToUpper())).ToListAsync();
            }

            //if (type == "usuario")
            //{
            //      lsReservaModel = await _context.ReservaModels.Include(unidad => unidad.ForKeyUnidadOrg_Reserva)
            //            .Include(diaReser => diaReser.ForKeyReservaDia_Reserva)
            //            .Where(sear => sear.nombre_usuario_reserva.ToUpper().Contains(search.ToUpper())).ToListAsync();
            //}

            foreach (var reserva in lsReservaModel)
            {
                lsReserva.Add(new ReservaDTO()
                {
                    id_reserva = reserva.id_reserva,
                    nombre_unidad_organizacional = reserva.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional.UpperFirstChar(),
                    nombre_submodulo = reserva.nombre_grupo,
                    nombre_usuario_colaborador = reserva.nombre_usuario_colaborador,
                    fecha_inicio_reserva = reserva.fecha_inicio_reserva,
                    fecha_fin_reserva = reserva.fecha_fin_reserva,
                    descripcion_reserva = reserva.descripcion_reserva.UpperFirstChar(),
                    estado_reserva = reserva.estado_reserva.UpperFirstChar(),
                    reservaDias = reserva.ForKeyReservaDia_Reserva.Select(reser => new ReservaDiaDTO()
                    {
                        reserva_dia_id = reser.reserva_dia_id,
                        id_reserva = reser.id_reserva,
                        reserva_dia_dia = reser.reserva_dia_dia.UpperFirstChar(),
                        reserva_dia_hora_inicio = reser.reserva_dia_hora_inicio,
                        jornada = reser.jornada
                        //reserva_dia_hora_fin = reser.reserva_dia_hora_fin
                    }).ToList()
                });
            }

            if (lsReserva.Count() == 0)
                throw new Exception(Enums.MessageNoRecord);

            return lsReserva;
        }

        public async Task SaveReserva(InsertarReservaDTO reserva)
        {
            try
            {     // Buscando existencia de reserva
                var reserva_dia_existente = _context.ReservaModels.Where(ent =>
                    ent.submodulo == reserva.submodulo &&
                    ent.nombre_programa == reserva.nombre_programa &&
                    ent.nombre_grupo == reserva.nombre_grupo &&
                    ent.nivel == reserva.nivel &&
                    ent.fecha_inicio_reserva.Equals(reserva.fecha_inicio_reserva) &&
                    ent.fecha_fin_reserva.Equals(reserva.fecha_fin_reserva)
                ).Select(ent => ent.id_reserva).ToList();

                if (reserva_dia_existente.Count != 0)
                {
                    long id_reserva = reserva_dia_existente.First();


                    reserva.reservaDia.ForEach(r =>
                    {
                        ReservaDiaModel reservas_dia = new ReservaDiaModel()
                        {
                            //reserva_dia_id = reserva.reservaDia.id_reserva,
                            id_reserva = id_reserva,
                            reserva_dia_dia = r.reserva_dia_dia,
                            reserva_dia_hora_inicio = r.reserva_dia_hora_inicio,
                            jornada = r.jornada
                            //reserva_dia_hora_fin = reserva.reservaDia.reserva_dia_hora_fin
                        };
                        _context.ReservaDiaModels.Add(reservas_dia);

                    });
                    await _context.SaveChangesAsync();

                }
                else
                {
                    var entidadMap = new ReservaModel()
                    {
                        //id_reserva = reserva.id_reserva,
                        id_unidad_organizacional = reserva.id_unidad_organizacional,
                        identificador_grupo = reserva.identificador_grupo,
                        nombre_grupo = reserva.nombre_grupo,
                        id_usuario_reserva = reserva.id_usuario_reserva,
                        fecha_inicio_reserva = reserva.fecha_inicio_reserva,
                        fecha_fin_reserva = reserva.fecha_fin_reserva,
                        descripcion_reserva = reserva.descripcion_reserva,
                        estado_reserva = reserva.estado_reserva,
                        id_usuario_colaborador = reserva.id_usuario_colaborador,
                        nombre_usuario_colaborador = reserva.nombre_usuario_colaborador,
                        nivel = reserva.nivel,
                        codigo_programa = reserva.codigo_programa,
                        nombre_programa = reserva.nombre_programa,
                        //id_rol = reserva.id_rol,
                        submodulo = reserva.submodulo,
                        //jornada = reserva.jornada
                    };

                    _context.ReservaModels.Add(entidadMap);
                    await _context.SaveChangesAsync();



                    reserva.reservaDia.ForEach(r =>
                    {
                        ReservaDiaModel reservas = new ReservaDiaModel()
                        {
                            //reserva_dia_id = reserva.reservaDia.id_reserva,
                            id_reserva = entidadMap.id_reserva,
                            reserva_dia_dia = r.reserva_dia_dia,
                            reserva_dia_hora_inicio = r.reserva_dia_hora_inicio,
                            jornada = r.jornada
                            //reserva_dia_hora_fin = reserva.reservaDia.reserva_dia_hora_fin
                        };
                        _context.ReservaDiaModels.Add(reservas);

                    });
                    await _context.SaveChangesAsync();

                }
            }
            catch (Exception e)
            {
                DebugHelper.Log(e.ToString());
                throw;
            }
        }

        public async Task UpdateReserva(InsertarReservaDTO reserva)
        {
            try
            {
                var existingReserva = await _context.ReservaModels.FirstOrDefaultAsync(sear => sear.id_reserva == reserva.id_reserva);
                if (existingReserva == null)
                {
                    throw new Exception(Enums.MessageDoesNotExist);
                }


                existingReserva.id_unidad_organizacional = reserva.id_unidad_organizacional;
                existingReserva.identificador_grupo = reserva.identificador_grupo;
                existingReserva.nombre_grupo = reserva.nombre_grupo.ToLower();
                existingReserva.id_usuario_reserva = reserva.id_usuario_reserva;
                existingReserva.fecha_inicio_reserva = reserva.fecha_inicio_reserva;
                existingReserva.fecha_fin_reserva = reserva.fecha_fin_reserva;
                existingReserva.descripcion_reserva = reserva.descripcion_reserva;
                existingReserva.estado_reserva = reserva.estado_reserva.ToLower();
                existingReserva.id_usuario_colaborador = reserva.id_usuario_colaborador;
                existingReserva.nombre_usuario_colaborador = reserva.nombre_usuario_colaborador.ToLower();
                existingReserva.nivel = reserva.nivel;
                existingReserva.codigo_programa = reserva.codigo_programa;
                existingReserva.nombre_programa = reserva.nombre_programa.ToLower();
                existingReserva.submodulo = reserva.submodulo;

                _context.Entry(existingReserva).State = EntityState.Modified;
                _context.Update(existingReserva);
                await _context.SaveChangesAsync();

                var reservas_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva).ToListAsync();
                foreach (var res_dia in reservas_dia)
                    _context.ReservaDiaModels.Remove(res_dia);

                await _context.SaveChangesAsync();

                foreach (var r in reserva.reservaDia)
                {
                    ReservaDiaModel newReservaDia = new ReservaDiaModel()
                    {
                        id_reserva = reserva.id_reserva,
                        reserva_dia_dia = r.reserva_dia_dia,
                        reserva_dia_hora_inicio = r.reserva_dia_hora_inicio,
                        jornada = r.jornada,
                        //reserva_dia_hora_fin = r.reserva_dia_hora_fin
                    };
                    _context.ReservaDiaModels.Add(newReservaDia);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //DebugHelper.Log(e.ToString());
                throw;
            }
        }




        public async Task<List<FiltroReservaDTO>> GetProgramaByEscuela(string id_submodulo)
        {
            List<FiltroReservaDTO> filtroReservaDTOs = new List<FiltroReservaDTO>();

            filtroReservaDTOs = await _context.ReservaModels.Where(x => x.submodulo == id_submodulo)
                  .GroupBy(x => new { x.nombre_programa, x.codigo_programa })
                  .Select(enti => new FiltroReservaDTO()
                  {
                      codigo = enti.Key.codigo_programa,
                      nombre = enti.Key.nombre_programa.UpperFirstChar(),
                      cantidad_registro = enti.Count()
                  }).OrderBy(x => x.nombre).ToListAsync();

            return filtroReservaDTOs;
        }

        public async Task<List<ReservaDTO>> GetByJornada(DateTime fechainicio, DateTime fechafin)
        {
            List<ReservaDTO> reservaDTOs = new List<ReservaDTO>();

            reservaDTOs = await _context.ReservaModels.Where(x => x.fecha_inicio_reserva >= fechainicio &&
                  x.fecha_fin_reserva <= fechafin).Select(reserva => new ReservaDTO()
                  {
                      id_reserva = reserva.id_reserva,
                      nombre_unidad_organizacional = reserva.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional.UpperFirstChar(),
                      nombre_submodulo = reserva.nombre_grupo,
                      nombre_usuario_colaborador = reserva.nombre_usuario_colaborador,
                      fecha_inicio_reserva = reserva.fecha_inicio_reserva,
                      fecha_fin_reserva = reserva.fecha_fin_reserva,
                      descripcion_reserva = reserva.descripcion_reserva.UpperFirstChar(),
                      estado_reserva = reserva.estado_reserva.UpperFirstChar(),
                      reservaDias = reserva.ForKeyReservaDia_Reserva.Select(reser => new ReservaDiaDTO()
                      {
                          reserva_dia_id = reser.reserva_dia_id,
                          id_reserva = reser.id_reserva,
                          reserva_dia_dia = reser.reserva_dia_dia.UpperFirstChar(),
                          reserva_dia_hora_inicio = reser.reserva_dia_hora_inicio,
                          jornada = reser.jornada
                          //reserva_dia_hora_fin = reser.reserva_dia_hora_fin
                      }).ToList()
                  }).ToListAsync();

            return reservaDTOs;
        }

        public async Task<List<ReporteHorario>> GetReportePrograma(string grupo, int nivel, string codigo_programa)
        {
            var lsReservaData = await _context.ReservaModels.Where(
                    x => x.nombre_grupo == grupo &&
                    x.nivel == nivel &&
                    x.codigo_programa == codigo_programa
                )
                  .Include(x => x.ForKeyUnidadOrg_Reserva)
                  .ToListAsync();

            var reporte_docente_lista = new List<ReporteHorario>();


            foreach (var obj in lsReservaData)
            {
                var reservas_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == obj.id_reserva)
                .Select(x => new ReporteHorario
                {
                    codigo_programa = obj.codigo_programa,
                    nombre_programa = obj.nombre_grupo,
                    nombre_grupo = obj.nombre_grupo,
                    fecha_inicio_reserva = obj.fecha_inicio_reserva,
                    reservaDias = new ReporteReservaDia
                    {
                        reserva_dia_dia = x.reserva_dia_dia,
                        reserva_dia_hora_inicio = x.reserva_dia_hora_inicio,
                    }
                }).ToListAsync();
                reporte_docente_lista.AddRange(reservas_dia);
            }

            //reporte_docente_lista.Sort(x => x.reporteReservaDiaDocentes.reserva_dia_hora_inicio)
            var lista_organizada = reporte_docente_lista.OrderBy(x => x.reservaDias.reserva_dia_hora_inicio).ToList();

            return lista_organizada;
        }

        public async Task<List<ReporteDocente>> GetReporteDocente(long id_colaborador)
        {
            var lsReservaData = await _context.ReservaModels.Where(x => x.id_usuario_colaborador == id_colaborador)
                  .Include(x => x.ForKeyUnidadOrg_Reserva)
                  .ToListAsync();

            var reporte_docente_lista = new List<ReporteDocente>();


            foreach (var obj in lsReservaData)
            {
                var reservas_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == obj.id_reserva)
                .Select(x => new ReporteDocente
                {
                    reserva_dia_dia = x.reserva_dia_dia,
                    reporteReservaDiaDocentes = new ReporteReservaDiaDocente
                    {
                        fecha_inicio_reserva = obj.fecha_inicio_reserva,
                        fecha_fin_reserva = obj.fecha_fin_reserva,
                        nombre_programa = obj.nombre_programa,
                        nombre_unidad_organizacional = obj.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional,
                        reserva_dia_hora_inicio = x.reserva_dia_hora_inicio,

                    }
                }).ToListAsync();
                reporte_docente_lista.AddRange(reservas_dia);
            }

            //reporte_docente_lista.Sort(x => x.reporteReservaDiaDocentes.reserva_dia_hora_inicio)
            var lista_organizada = reporte_docente_lista.OrderBy(x => x.reporteReservaDiaDocentes.reserva_dia_hora_inicio).ToList();

            return lista_organizada;
        }

        public async Task<List<ComboDTO>> GetComboUsuarioColaborador()
        {
            var lsCombo = await _context.ReservaModels
                  .GroupBy(x => new { x.nombre_usuario_colaborador, x.id_usuario_colaborador })
                  .Select(enti => new ComboDTO()
                  {
                      value = enti.Key.id_usuario_colaborador.Value,
                      label = enti.Key.nombre_usuario_colaborador
                  }).ToListAsync();

            return lsCombo;
        }

        public async Task<List<ReservaDTO>> GetfiltrarUsuariosPorRol(long nivel_rol, string area_rol)
        {
            var lista_reservas = new List<ReservaDTO>();
            var manejador_reservas = new List<ReservaModel>();

            // obteniendo todos los roles en el area dada
            var roles = await _context.RolModels.Where(x => x.area_rol == area_rol && x.nivel_rol >= nivel_rol)
                .Select(x => x.id_rol)
                .ToListAsync();

            var usuarios_id_rol = await _context.UsuarioModels.Where(x => roles.Contains(x.id_rol))
                .Select(x => x.id_usuario).ToListAsync();

            var reservas = await _context.ReservaModels.Where(x => usuarios_id_rol.Contains((long)x.id_usuario_reserva) && x.estado_reserva == "activo")
            .Select(reserva => new ReservaDTO()
            {
                id_reserva = reserva.id_reserva,
                nombre_unidad_organizacional = reserva.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional.UpperFirstChar(),
                nombre_submodulo = reserva.nombre_grupo,
                nombre_usuario_colaborador = reserva.nombre_usuario_colaborador,
                fecha_inicio_reserva = reserva.fecha_inicio_reserva,
                fecha_fin_reserva = reserva.fecha_fin_reserva,

            }).ToListAsync();

            return reservas;

        }


        public async Task<InformeNombreEspacio> GetContarNombreEspacio(long id_sede, long id_tipo_espacio)
        {
            var list_informes_dia = new List<InformeDia>();

            var espacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_sede &&
                  x.estado_unidad_organizacional == "activo" && x.id_tipo_espacio == id_tipo_espacio).Select(x => x.id_unidad_organizacional).ToListAsync();

            var nombre_espacio = await _context.TipoEspacioModels.Where(x => x.id_tipo_espacio == id_tipo_espacio)
                .Select(x => x.nombre_tipo_espacio).FirstAsync();

            var reservas = await _context.ReservaModels.Where(x => x.estado_reserva.ToLower() == "activo" && espacios.Contains(x.id_unidad_organizacional))
                  .Select(x => x.id_reserva).ToListAsync();

            foreach (var reserva in reservas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva).ToListAsync();
                foreach (var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes" && !list_informes_dia.Any(x => x.dia == "Lunes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Lunes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Martes" && !list_informes_dia.Any(x => x.dia == "Martes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Martes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Miércoles" && !list_informes_dia.Any(x => x.dia == "Miércoles"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Jueves" && !list_informes_dia.Any(x => x.dia == "Jueves"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Jueves").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Viernes" && !list_informes_dia.Any(x => x.dia == "Viernes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Viernes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Sábado" && !list_informes_dia.Any(x => x.dia == "Sábado"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Sábado").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Domingo" && !list_informes_dia.Any(x => x.dia == "Domingo"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Domingo").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                }
            }

            var informe = new InformeNombreEspacio
            {
                nombre_espacio = nombre_espacio,
                cantidad_tipoespacio = espacios.Count,
                Dias = list_informes_dia
            };


            return informe;
        }



        public async Task<InformeOcupacionSede> GetContarOcupacionAulas(long id_unidad_organizacional_padre)
        {
            var list_informes_dia = new List<InformeDia>();
            
            var conteo_espacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_unidad_organizacional_padre &&
                  x.estado_unidad_organizacional == "activo").Select(x => x.id_unidad_organizacional).CountAsync();

            var espacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_unidad_organizacional_padre &&
                  x.estado_unidad_organizacional == "activo").Select(x => x.id_unidad_organizacional).ToListAsync();

            var reservas = await _context.ReservaModels.Where(x => x.estado_reserva == "activo" && espacios.Contains(x.id_unidad_organizacional))
                  .Select(x => x.id_reserva).ToListAsync();

            foreach(var reserva in reservas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva).ToListAsync();
                foreach(var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes" && !list_informes_dia.Any(x => x.dia == "Lunes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Lunes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Martes" && !list_informes_dia.Any(x => x.dia == "Martes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Martes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Miércoles" && !list_informes_dia.Any(x => x.dia == "Miércoles"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Jueves" && !list_informes_dia.Any(x => x.dia == "Jueves"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Jueves").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Viernes" && !list_informes_dia.Any(x => x.dia == "Viernes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Viernes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Sábado" && !list_informes_dia.Any(x => x.dia == "Sábado"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Sábado").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Domingo" && !list_informes_dia.Any(x => x.dia == "Domingo"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Domingo").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                }
            }

            var informe_ocupacion = new InformeOcupacionSede
            {
                ocupacion_total = conteo_espacios,
                Dias = list_informes_dia
            };

            
            return informe_ocupacion;
        }

        public async Task<InformeUnidadesReservadas> GetUnidadesReservadas(long id_unidad_organizacional)
        {

            var list_informes_dia = new List<InformeDia>();
            var unidades_reservadas = await _context.ReservaModels.Where(x =>
                x.id_unidad_organizacional == id_unidad_organizacional &&
                x.estado_reserva.ToLower() == "activo"
            ).Select(x => new
            {
                id_reserva = x.id_reserva,
                nombre_unidad = x.ForKeyUnidadOrg_Reserva.nombre_unidad_organizacional
            }).ToListAsync();

            if (unidades_reservadas.Count == 0)
            {
                throw new Exception("No existen unidades reservadas");
            }

            foreach (var reserva in unidades_reservadas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva).ToListAsync();
                foreach (var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes" && !list_informes_dia.Any(x => x.dia == "Lunes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Lunes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Martes" && !list_informes_dia.Any(x => x.dia == "Martes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Martes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Miércoles" && !list_informes_dia.Any(x => x.dia == "Miércoles"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Jueves" && !list_informes_dia.Any(x => x.dia == "Jueves"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Jueves").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Viernes" && !list_informes_dia.Any(x => x.dia == "Viernes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Viernes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Sábado" && !list_informes_dia.Any(x => x.dia == "Sábado"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Sábado").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Domingo" && !list_informes_dia.Any(x => x.dia == "Domingo"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Domingo").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                }
            }

            var informe = new InformeUnidadesReservadas
            {
                nombre_unidad_organizacional = unidades_reservadas.First().nombre_unidad,
                Dias = list_informes_dia
            };


            return informe;
        }

        public async Task<InformeCodigoPrograma> GetByCodigo(long id_sede, string codigo)
        {
            var list_informes_dia = new List<InformeDia>();
            var lsEspacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_sede &&
                    x.estado_unidad_organizacional == "activo").Select(x => x.id_unidad_organizacional).ToListAsync();

            if (lsEspacios.Count == 0)
            {
                throw new Exception("No hay registros disponibles vinculados a id_sede");
            }

            var reservas = await _context.ReservaModels.Where(x => x.estado_reserva.ToLower() == "activo" && lsEspacios.Contains(x.id_unidad_organizacional) && x.codigo_programa.ToLower() == codigo.ToLower())
                  .Select(x => x.id_reserva).ToListAsync();

            foreach (var reserva in reservas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva).ToListAsync();
                foreach (var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes" && !list_informes_dia.Any(x => x.dia == "Lunes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Lunes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Lunes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Martes" && !list_informes_dia.Any(x => x.dia == "Martes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Martes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Martes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Miércoles" && !list_informes_dia.Any(x => x.dia == "Miércoles"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Miércoles").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Jueves" && !list_informes_dia.Any(x => x.dia == "Jueves"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Jueves").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Jueves").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Viernes" && !list_informes_dia.Any(x => x.dia == "Viernes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Viernes").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Viernes").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Sábado" && !list_informes_dia.Any(x => x.dia == "Sábado"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Sábado").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Sábado").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                    if (dia.reserva_dia_dia == "Domingo" && !list_informes_dia.Any(x => x.dia == "Domingo"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Domingo").CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Domingo").CountAsync();
                        list_informes_dia.Add(new InformeDia
                        {
                            dia = dia.reserva_dia_dia,
                            jornada1 = conteo_jornada1,
                            jornada2 = conteo_jornada2,
                            jornada3 = conteo_jornada3,
                            jornada4 = conteo_jornada4,
                            jornada5 = conteo_jornada5
                        });
                        continue;
                    }
                }
            }

            var informe = new InformeCodigoPrograma
            {
                cantidad_total_espacios = lsEspacios.Count,
                codigo_programa = codigo,
                Dias = list_informes_dia
            };

            return informe;
        }

        public async Task<List<InformeOcupacionSede>> GetContarOcupacionTodosEspacios()
        {
            var lista_informes_ocupacion = new List<InformeOcupacionSede>();

            var unidades_orgs = await _context.UnidadOrganizacionalModels
                .Select(x => x.id_unidad_organizacional_padre).ToListAsync();

            foreach(var uni in unidades_orgs)
            {
                var informe = await this.GetContarOcupacionAulas(5);
                lista_informes_ocupacion.Add(informe);
            }

            return lista_informes_ocupacion;
        }

        //public async Task<Informe|OcupacionTipoEspacio> GetContarOcupacionPorTipoEspacio(long id_sede)
        //{
        //      List<long> lsEspacios = new List<long>();
        //      int lsReservasDias = 0;
        //      List<long> lsCantidadReservasDias = new List<long>();

        //      lsEspacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_sede &&
        //            x.estado_unidad_organizacional == "activo").ToListAsync();





        //      foreach (var item in lsTipoEspacio)
        //      {

        //      }

        //      var lsReservas = await _context.ReservaModels.Where(x => x.estado_reserva == "activo" && lsEspacios.Contains(x.id_unidad_organizacional))
        //            .Select(x => new { reserva = x.id_reserva, jornada = x.jornada }).ToListAsync();




        //      int jornada1 = 0;
        //      int jornada2 = 0;
        //      int jornada3 = 0;
        //      int jornada4 = 0;
        //      int jornada5 = 0;

        //      foreach (var item in lsReservas)
        //      {
        //            var contador = 0;
        //            contador = await _context.ReservaDiaModels.Where(x => x.id_reserva == item.reserva).CountAsync();

        //            if (item.jornada.ToLower() == Enums.jornada1)
        //            {
        //                  jornada1 += contador;
        //            }
        //            else if (item.jornada.ToLower() == Enums.jornada2)
        //            {
        //                  jornada2 += contador;
        //            }
        //            else if (item.jornada.ToLower() == Enums.jornada3)
        //            {
        //                  jornada3 += contador;
        //            }
        //            else if (item.jornada.ToLower() == Enums.jornada4)
        //            {
        //                  jornada4 += contador;
        //            }
        //            else if (item.jornada.ToLower() == Enums.jornada5)
        //            {
        //                  jornada5 += contador;
        //            }

        //            lsReservasDias = lsReservasDias + contador;
        //      }

        //      var informe = new InformeOcupacionSede()
        //      {
        //            cantidad_tipoespacio = lsEspacios.Count(),
        //            cantidad_reserva = lsReservasDias,
        //            jornada1 = jornada1,
        //            jornada2 = jornada2,
        //            jornada3 = jornada3,
        //            jornada4 = jornada4,
        //            jornada5 = jornada5
        //      };

        //      return informe;
        //}

        //public async Task<List<FiltroReservaDTO>> GetNivelByPrograma(long id_escuela, string nombre, string codigo)
        //{
        //      List<FiltroReservaNivelDTO> filtroReservaNivelDTOs = new List<FiltroReservaNivelDTO>();

        //      filtroReservaNivelDTOs = await _context.ReservaModels.Where(x => x.escuela == id_escuela &&
        //            x.nombre_programa.ToUpper().Equals(nombre.ToUpper()) && x.codigo_programa.ToUpper().Equals(codigo.ToUpper()))
        //            .GroupBy(x => x.nivel)
        //            .Select(async enti => new FiltroReservaNivelDTO()
        //            {
        //                  nombre = Convert.ToInt32(enti.Key),
        //                  cantidad = enti.Count(),
        //                  grupos = await _context.ReservaModels.Where(x => x.escuela == id_escuela &&
        //                  x.nombre_programa.ToUpper().Equals(nombre.ToUpper()) && x.codigo_programa.ToUpper().Equals(codigo.ToUpper()) &&
        //                  x.nivel == Convert.ToInt32(enti.Key)).GroupBy(x => x.nombre_programa)


        //            }).ToListAsync();
        //}
    }
}