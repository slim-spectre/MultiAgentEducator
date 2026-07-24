

using System.Net.Http.Headers;
using System.Text.Json;

public class OpenRouterService : IOpenRouterService
{
    private  HttpClient _client;
    private IConfiguration _config;

    private string _openRouterApiKey;

    public OpenRouterService(HttpClient httpClient,IConfiguration configuration)
    {
        _client = httpClient;
        _config = configuration;
    
        _openRouterApiKey  = _config["OpenRouterApiKey"];

        _client.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openRouterApiKey);

    }

    public async Task<string> GetCompletionAsync(string userPrompt,string systemInstruction,string model)
    {
        var requestDto = new OpenRouterRequestDto
        {
            Model = model,
            Messages = new List<OpenRouterMessageDto>
            {
                new OpenRouterMessageDto { Role = "system", Content = systemInstruction },
                new OpenRouterMessageDto { Role = "user", Content = userPrompt }
            }
        };
        var response = await _client.PostAsJsonAsync("chat/completions",requestDto);
        var rawJson = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error from OpenRouter ({response.StatusCode}): {rawJson}");
        }
         using var doc = JsonDocument.Parse(rawJson);
            var responseText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

        return responseText ?? string.Empty;
    }

}