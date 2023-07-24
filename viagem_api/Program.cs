using Microsoft.EntityFrameworkCore;
using viagem_api.Data;

var builder = WebApplication.CreateBuilder(args);

// String de conex�o com o banco de dados 
var connectionString = builder.Configuration.GetConnectionString("ViagemConnection");

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adi��o dos servi�os do NPGSQL
builder.Services.AddDbContext<ViagemContext>(configuracoes =>
    configuracoes.UseLazyLoadingProxies().UseNpgsql(connectionString));

// Adi��o das configura��es do CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseCors();

app.Run();
