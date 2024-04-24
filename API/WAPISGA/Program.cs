using Microsoft.OpenApi.Models;
using System.Reflection;
using WAPISGA.Middleware;

var builder = WebApplication.CreateBuilder(args);

var CorsPolicy = "_PermitirOrigemEspecifica";

//Cors Genérico necessário para o framework
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
        policy =>
        {
            policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Configura o caminho do XML com a documentação para a API do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API SGA", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseRouting();

app.UseCors(CorsPolicy);
app.UseCorsMiddle();

app.UseAuthentication();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API SGA");
});
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
