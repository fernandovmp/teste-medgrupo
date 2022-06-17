using AppContatos.Aplicacao.Extensoes;
using AppContatos.Infraestrutura.Extensoes;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AdicionarBancoDeDados(connectionString);
builder.Services.AdicionarCasosDeUso();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
