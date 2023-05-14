using CESDE.Application.Ports;
using CESDE.DataAdapter.repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CESDE.DataAdapter
{
      public static class Infraestructure
      {
            public static void AddRegisterCESDE_DbContext(this IServiceCollection services, IConfiguration configuration)
            {
                var connectionString = configuration.GetConnectionString("Conexion");
                 
                services.AddDbContext<CESDE_Context>(builder =>
                {
                    builder.UseSqlServer(connectionString);
                }, ServiceLifetime.Transient);

            services.AddHttpContextAccessor();
                  services.AddConfigureJWT(configuration);
                  services.AddRepositories();
            }

            public static void AddRepositories(this IServiceCollection services)
            {
                  services.AddScoped<ILoginRepositoryPort, LoginRepositoryAdapter>();
                  services.AddScoped<IDepartamentoRepositoryPort, DepartamentoRepositoryAdapter>();
                  services.AddScoped<IMunicipioRepositoryPort, MunicipioRepositoryAdapter>();
                  services.AddScoped<ITipoEspacioRepositoryPort, TipoEspacioRepositoryAdapter>();
                  services.AddScoped<ICaracteristicaRepositoryPort, CaracteristicaRepositoryAdapter>();
                  services.AddScoped<IUnidadOrganizacionalRepositoryPort, UnidadOrganizacionalRepositoryAdapter>();
                  services.AddScoped<IComponenteRepositoryPort, ComponenteRepositoryAdapter>();
                  services.AddScoped<IFuncionalidadRepositoryPort, FuncionalidadRepositoryAdapter>();
                  services.AddScoped<IReservaRepositoryPort, ReservaRepositoryAdapter>();
                  services.AddScoped<IRolEspacioRepositoryPort, RolEspacioRepositoryAdapter>();
                  services.AddScoped<IUnidadRolRepositoryPort, UnidadRolRepositoryAdapter>();
            }
      }
}