using System.Net.Http.Headers;
using System.Text.Json;

namespace MultiAgentTeacher.Api.Endpoints;

public static class AiTesterEndpoints
{
    private readonly static string model = "qwen/qwen-2.5-coder-32b-instruct:free";
    public static void MapAiTesterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/aiTester");

        group.MapPost("/test-code", async (AiTesterDto testerDto, IOpenRouterService service) =>
        {
            try
            {
                const string systemInstruction = "You are tester ai agent, answer shortly and coherently.";

                var resultText = await service.GetCompletionAsync(
                    userPrompt : testerDto.UserCode,
                    systemInstruction : systemInstruction,
                    model:model
                );
                return Results.Ok(resultText);
                
            }catch(Exception ex)
            {
                return Results.BadRequest(new
                {
                    Error = "Runtime error",
                    Details = ex.Message
                });
            }
        });
    }
}