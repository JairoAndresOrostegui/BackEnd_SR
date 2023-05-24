using CESDE.Application.Ports;
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


namespace CESDE.DataAdapter.helpers
{
    public class InformesHelper
    {
        public static async Task<InformeUnidadesReservadas> ObtenerDiasInforme(long id_unidad_organizacional, CESDE_Context _context)
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

            var cantidad_espacios = await _context.UnidadOrganizacionalModels.Where(x => x.estado_unidad_organizacional == "activo" && x.id_unidad_organizacional == id_unidad_organizacional)
                .Select(x => x.id_unidad_organizacional_padre).CountAsync();

            var cantidad_reservadas = await _context.ReservaModels.Where(x =>
                x.id_unidad_organizacional == id_unidad_organizacional &&
                x.estado_reserva.ToLower() == "activo"
            ).CountAsync();


            foreach (var reserva in unidades_reservadas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva).ToListAsync();
                foreach (var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes" && !list_informes_dia.Any(x => x.dia == "Lunes"))
                    {
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Lunes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Lunes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Lunes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Lunes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Lunes" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Martes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Martes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Martes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Martes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Martes" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Miércoles" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Miércoles" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Miércoles" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Miércoles" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Miércoles" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Jueves" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Jueves" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Jueves" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Jueves" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Viernes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Viernes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Viernes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Viernes" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Viernes" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Sábado" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Sábado" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Sábado" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Sábado" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Sábado" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                        var conteo_jornada1 = await _context.ReservaDiaModels.Where(x => x.jornada == "01" && x.reserva_dia_dia == "Domingo" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada2 = await _context.ReservaDiaModels.Where(x => x.jornada == "02" && x.reserva_dia_dia == "Domingo" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada3 = await _context.ReservaDiaModels.Where(x => x.jornada == "03" && x.reserva_dia_dia == "Domingo" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada4 = await _context.ReservaDiaModels.Where(x => x.jornada == "04" && x.reserva_dia_dia == "Domingo" && x.id_reserva == reserva.id_reserva).CountAsync();
                        var conteo_jornada5 = await _context.ReservaDiaModels.Where(x => x.jornada == "05" && x.reserva_dia_dia == "Domingo" && x.id_reserva == reserva.id_reserva).CountAsync();
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
                //nombre_unidad_organizacional = unidades_reservadas.First().nombre_unidad,
                Dias = list_informes_dia
            };


            return informe;
        }
    }
}
