public class OpenRouterService
{
    private  HttpClient _client;
    private IConfiguration _config;

    public OpenRouterService(HttpClient httpClient,IConfiguration configuration)
    {
        _client = httpClient;
        _config = configuration;
    }
}