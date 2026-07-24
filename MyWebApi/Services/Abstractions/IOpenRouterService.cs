public interface IOpenRouterService
{
    public Task<string> GetCompletionAsync (string userPrompt,string systemInstruction,string model);
}