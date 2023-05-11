using CESDE.DataAdapter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string CESDE_CORS = "_CESDE_CORS";
builder.Services.AddConfigureCORS(CESDE_CORS);
builder.Services.AddRegisterCESDE_DbContext(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CESDE_CORS);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseStaticFiles();