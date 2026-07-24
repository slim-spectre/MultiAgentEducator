using MultiAgentTeacher.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IOpenRouterService, OpenRouterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapAiTesterEndpoints();
app.Run();
