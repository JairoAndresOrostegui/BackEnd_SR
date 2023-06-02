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
                //Dias = list_informes_dia
            };


            return informe;
        }

        public static async Task<InformeOcupacionSede> ObtenerSede(long id_unidad_organizacional, CESDE_Context _context)
        {
      
            var espacios = await _context.UnidadOrganizacionalModels.Where(x => x.id_unidad_organizacional_padre == id_unidad_organizacional &&
                  x.estado_unidad_organizacional.ToLower() == "activo").Select(x => x.id_unidad_organizacional).ToListAsync();

            var reservas = await _context.ReservaModels.Where(x => x.estado_reserva.ToLower() == "activo" && espacios.Contains(x.id_unidad_organizacional))
                  .ToListAsync();

            int lunes_conteo_jornada1 = 0;
            int lunes_conteo_jornada2 = 0;
            int lunes_conteo_jornada3 = 0;

            int martes_conteo_jornada1 = 0;
            int martes_conteo_jornada2 = 0;
            int martes_conteo_jornada3 = 0;

            int miercoles_conteo_jornada1 = 0;
            int miercoles_conteo_jornada2 = 0;
            int miercoles_conteo_jornada3 = 0;

            int jueves_conteo_jornada1 = 0;
            int jueves_conteo_jornada2 = 0;
            int jueves_conteo_jornada3 = 0;

            int viernes_conteo_jornada1 = 0;
            int viernes_conteo_jornada2 = 0;
            int viernes_conteo_jornada3 = 0;

            int sadado_conteo_jornada4 = 0;
            int domingo_conteo_jornada5 = 0;

            var contador = 0;


            foreach (var reserva in reservas)
            {
                var reserva_dia = await _context.ReservaDiaModels.Where(x => x.id_reserva == reserva.id_reserva).ToListAsync();
                foreach (var dia in reserva_dia)
                {
                    if (dia.reserva_dia_dia == "Lunes")
                    {
                        if (dia.jornada == "01")
                        {
                            lunes_conteo_jornada1++;

                        }
                        else if (dia.jornada == "02")
                        {
                            lunes_conteo_jornada2++;
                        }
                        else if (dia.jornada == "03")
                        {
                            lunes_conteo_jornada3++;
                        }

                    }
                    else if (dia.reserva_dia_dia == "Martes")
                    {
                        if (dia.jornada == "01")
                        {
                            martes_conteo_jornada1++;

                        }
                        else if (dia.jornada == "02")
                        {
                            martes_conteo_jornada2++;
                        }
                        else if (dia.jornada == "03")
                        {
                            martes_conteo_jornada3++;
                        }

                    }
                    else if (dia.reserva_dia_dia == "Miércoles")
                    {
                        if (dia.jornada == "01")
                        {
                            miercoles_conteo_jornada1++;
                        }
                        else if (dia.jornada == "02")
                        {
                            miercoles_conteo_jornada2++;
                        }
                        else if (dia.jornada == "03")
                        {
                            miercoles_conteo_jornada3++;
                        }
                    }
                    else if (dia.reserva_dia_dia == "Jueves")
                    {
                        if (dia.jornada == "01")
                        {
                            jueves_conteo_jornada1++;
                        }
                        else if (dia.jornada == "02")
                        {
                            jueves_conteo_jornada2++;
                        }
                        else if (dia.jornada == "03")
                        {
                            jueves_conteo_jornada3++;
                        }
                    }
                    else if (dia.reserva_dia_dia == "Viernes")
                    {
                        if (dia.jornada == "01")
                        {
                            viernes_conteo_jornada1++;
                        }
                        else if (dia.jornada == "02")
                        {
                            viernes_conteo_jornada2++;
                        }
                        else if (dia.jornada == "03")
                        {
                            viernes_conteo_jornada3++;
                        }
                    }
                    else if (dia.reserva_dia_dia == "Sábado")
                    {
                        sadado_conteo_jornada4++;
                    }
                    else if (dia.reserva_dia_dia == "Domingo")
                    {
                        domingo_conteo_jornada5++;
                    }
                    else
                    {
                        contador++;
                    }
                }
            }
            var informe = new InformeOcupacionSede
            {
                //ocupacion_total = conteo_espacios,
                lunes_conteo_jornada1 = lunes_conteo_jornada1,
                lunes_conteo_jornada2 = lunes_conteo_jornada2,
                lunes_conteo_jornada3 = lunes_conteo_jornada3,

                martes_conteo_jornada1 = martes_conteo_jornada1,
                martes_conteo_jornada2 = martes_conteo_jornada2,
                martes_conteo_jornada3 = martes_conteo_jornada3,

                miercoles_conteo_jornada1 = miercoles_conteo_jornada1,
                miercoles_conteo_jornada2 = miercoles_conteo_jornada2,
                miercoles_conteo_jornada3 = miercoles_conteo_jornada3,

                jueves_conteo_jornada1 = jueves_conteo_jornada1,
                jueves_conteo_jornada2 = jueves_conteo_jornada2,
                jueves_conteo_jornada3 = jueves_conteo_jornada3,

                viernes_conteo_jornada1 = viernes_conteo_jornada1,
                viernes_conteo_jornada2 = viernes_conteo_jornada2,
                viernes_conteo_jornada3 = viernes_conteo_jornada3,

                sadado_conteo_jornada4 = sadado_conteo_jornada4,
                domingo_conteo_jornada5 = domingo_conteo_jornada5
            };

            return informe;
        }

    }
}
