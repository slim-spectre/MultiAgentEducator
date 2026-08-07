public interface IAgentManagerService
{
    public Task<string> ProcessAgentRequestAsync (AgentType type,string userPrompt);
}