namespace Endurist.Models;

public class OperationResult<TData>
{
    public TData Data { get; set; }

    public string Error { get; set; }
}
