using System.Net.Http.Headers;
using System.Text.Json;

namespace MultiAgentTeacher.Api.Endpoints;

public static class AiTesterEndpoints
{
    public static void MapAiTesterEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/aiTester");

        group.MapPost("/test-code", async (AiTesterDto testerDto, IConfiguration configuration, HttpClient client) =>
        {
            var apiKey = configuration["OpenRouterApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                return Results.Problem("OpenRouterApiKey не знайдено в конфігурації!");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            const string requestUrl = "https://openrouter.ai/api/v1/chat/completions";

            var payload = new
            {
                model = "nvidia/nemotron-3-ultra-550b-a55b:free",
                messages = new[]
                {
                    new 
                    { 
                        role = "system", 
                        content = "You are a code tester assistant. Analyze the user's code and provide short feedback." 
                    },
                    new 
                    { 
                        role = "user", 
                        content = testerDto.UserCode 
                    }
                }
            };

            var response = await client.PostAsJsonAsync(requestUrl, payload);
            var rawJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return Results.BadRequest(new
                {
                    Error = "Error from OpenRouter API",
                    StatusCode = (int)response.StatusCode,
                    Details = rawJson
                });
            }
            using var doc = JsonDocument.Parse(rawJson);
            var responseText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Results.Ok(responseText);
        });
    }
}