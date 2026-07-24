using System.Net.Http.Headers;
using System.Text.Json;

namespace MultiAgentTeacher.Api.Endpoints;

public static class AiTesterEndpoints
{
    public static void MapAiTesterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/aiTester");

        group.MapPost("/test-code", async (AiTesterDto testerDto, IOpenRouterService service) =>
        {
            try
            {
                var model = "nvidia/nemotron-3-ultra-550b-a55b:free";
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