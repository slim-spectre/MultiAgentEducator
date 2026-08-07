public class AgentManagerService : IAgentManagerService
{
    private readonly IOpenRouterService _openRouterService;
    private readonly IAgentPromptService _promptService;
    private readonly string model = "nvidia/nemotron-3-ultra-550b-a55b:free";

    public AgentManagerService(IOpenRouterService routerService,IAgentPromptService promptService)
    {
        _openRouterService = routerService;
        _promptService = promptService;
    }
    public async Task<string> ProcessAgentRequestAsync (AgentType type,string UserPrompt)
    {
        var prompt = _promptService.GetPromptOfAgent(type);

        var resultText = await _openRouterService.GetCompletionAsync(
                    userPrompt : UserPrompt,
                    systemInstruction : prompt,
                    model:model
                );
        return resultText;

    }
}