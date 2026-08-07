namespace MultiAgentTeacher.Api.Endpoints;


public static class AgentEndpoints
{
    public static void MapAgentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/agents");

        group.MapPost("/ask",async (AgentRequestDto agentDto,IAgentManagerService managerService) =>
        {
            var result = await managerService.ProcessAgentRequestAsync(agentDto.Type,agentDto.Input);
            return Results.Ok(result);
        });


    }
}