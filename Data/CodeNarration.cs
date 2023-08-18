using System.Text;
using System.Web;

namespace AutoDoc_Front_End.Data;

public class FunctionArray
{
    public string original_code { get; set; }
    public string function_name { get; set; }
    public List<InputParametersArray> input_parameters_array { get; set; }
    public string functionDecription => JoinDescription();
    public string return_type { get; set; }
    public string example { get; set; }
    public List<string> explanation_steps_array { get; set; }

    private string JoinDescription(){
        
        var sb = new StringBuilder();
        // explanation_steps_array.ForEach(s => sb.Append($"<p>{s}</p>"));
        explanation_steps_array.ForEach(s => sb.Append($"{s} "));
        return sb.ToString();
    }
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