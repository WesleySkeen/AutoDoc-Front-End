namespace AutoDoc_Front_End.Data;

public class FunctionArray
{
    public string original_code { get; set; }
    public string function_name { get; set; }
    public List<InputParametersArray> input_parameters_array { get; set; }
    public string return_type { get; set; }
    public string example { get; set; }
    public List<string> explanation_steps_array { get; set; }
}

public class InputParametersArray
{
    public string parameter_name { get; set; }
    public string parameter_type { get; set; }
}

public class DocumentItem
{
    public string mermaid { get; set; }
    public List<FunctionArray> function_array { get; set; }
}