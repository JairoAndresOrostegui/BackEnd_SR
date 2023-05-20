using CESDE.Application.UseCases;

using Microsoft.Extensions.DependencyInjection;

namespace CESDE.Application
{
    public static class Infraestructure
    {
        public static void AddRegisterUseCase(this IServiceCollection services)
        {
            services.AddScoped<LoginUseCase>();
            services.AddScoped<DepartamentoUseCase>();
            services.AddScoped<MunicipioUseCase>();
            services.AddScoped<TipoEspacioUseCase>();
            services.AddScoped<UnidadOrganizacionalUseCase>();
            services.AddScoped<CaracteristicaUseCase>();
            services.AddScoped<FuncionalidadUseCase>();
            services.AddScoped<ComponenteUseCase>();
            services.AddScoped<ReservaUseCase>();
            services.AddScoped<RolEspacioUseCase>();
            services.AddScoped<UnidadRolUseCase>();
        }
    }
}
