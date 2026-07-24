public class OpenRouterRequestDto
{
    public string Model {get;set;} = string.Empty;
    public List<OpenRouterMessageDto>? Messages {get;set;}
}