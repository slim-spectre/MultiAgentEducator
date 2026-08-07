public class AgentPromptService : IAgentPromptService
{
    public string GetPromptOfAgent(AgentType type) => type switch
    {
        AgentType.CodeReviewer => "Ти строгий Senior Developer. Проаналізуй код користувача. Вкажи на баги, потенційні витоки пам'яті та запропонуй покращення за принципами SOLID.",
        AgentType.TheoryMentor => "Ти викладач програмування. Поясни тему/концепцію, яку надав користувач, за допомогою простих життєвих аналогій та коротких прикладів коду.",
        AgentType.TaskGenerator => "Ти методист з розробки курсів. Згенеруй практичне завдання для користувача за вказаною темою із 3 рівнями складності (Easy, Medium, Hard).",
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid type of agent: {type}")
    };
}

