using MultiAgentTeacher.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IOpenRouterService, OpenRouterService>();
builder.Services.AddScoped<IAgentManagerService,AgentManagerService>();
builder.Services.AddScoped<IAgentPromptService,AgentPromptService>();
builder.Services.AddHttpClient<IOpenRouterService, OpenRouterService>(client =>
{
    client.Timeout = TimeSpan.FromMinutes(2);
})
.AddStandardResilienceHandler();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapAiTesterEndpoints();
app.MapAgentEndpoints();
app.Run();
