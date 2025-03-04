using Microsoft.AspNetCore.Diagnostics;
using Sorteio.Aplicacao;
using Sorteio.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AdicionarInfra(builder.Configuration);
builder.Services.AdicionarAplicacao();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async contexto =>
    {
        contexto.Response.ContentType = "application/json";

        var excecao = contexto.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (excecao != null)
        {
            var statusCode = excecao switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,  
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError         
            };

            contexto.Response.StatusCode = statusCode;

            var resposta = new
            {
                status = statusCode,
                mensagem = excecao.Message
            };

            await contexto.Response.WriteAsJsonAsync(resposta);
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();