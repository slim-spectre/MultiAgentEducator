public interface IOpenRouterService
{
    public Task<string> GetCompletionAsync (string userPrompt,string systemInstruction,string model);
    public IAsyncEnumerable<string> GetCompletionStreamAsync (string userPrompt, string systemInstruction, string model);
}