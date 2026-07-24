

using System.Text.Json;

namespace MultiAgentTeacher.Api.Endpoints;

public static class AiTesterEndpoints
{
    public static void MapAiTesterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("aiTester");


        group.MapPost("/test-code",async (AiTesterDto testerDto,IConfiguration configuration,HttpClient client) =>
        {
            var apiKey = configuration["GeminiApiKey"];
            if (!client.DefaultRequestHeaders.Contains("x-goog-api-key"))
            {
                client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);
            }
            const string requestUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

            var payload = new
            {
                contents = new[]    
                {
                    new {parts = new[] { new {text = testerDto.UserCode }}}
                },
                systemInstruction = new
                {
                parts = new[] { new { text = "You are tester ai assistent,answer shortly." } }
                }
            };

            var response = await client.PostAsJsonAsync(requestUrl,payload);
            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            var responseText = doc.RootElement
                            .GetProperty("candidates")[0]
                            .GetProperty("content")
                            .GetProperty("parts")[0]
                            .GetProperty("text")
                            .GetString();
            return Results.Ok(responseText);

        });
    }
}