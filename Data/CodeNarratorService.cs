namespace AutoDoc_Front_End.Data;

public class CodeNarratorService
{
    public Task<CodeNarration[]> GetCodeNarrationAsync(DateOnly startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new CodeNarration
        {
            Description = "This is a description",
            FunctionName = "This is a function name",
            Code = "This is some code"
        }).ToArray());
    }
}
