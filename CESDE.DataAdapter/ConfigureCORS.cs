using Microsoft.Extensions.DependencyInjection;

namespace CESDE.DataAdapter
{
      public static class ConfigureCORS
      {
            public static void AddConfigureCORS(this IServiceCollection services, string cesdeCORS)
            {
                  services.AddCors(options =>
                  {
                        options.AddPolicy(name: cesdeCORS,
                              builder =>
                              {
                                    builder.WithOrigins("*")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                              });
                  });
            }
      }
}

// DESARROLLO = http://localhost:4200
// PRODUCCIÓN = http://190.217.58.171:55480
