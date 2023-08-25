namespace Framework.Core.ApplicationServices.Services;
public abstract class ApplicationServiceResult : IApplicationServiceResult
{
    private readonly List<string> _messages = new();

    public IEnumerable<string> Messages => _messages;

    public ApplicationServiceStatus Status { get; set; }

    public void AddMessage(string error) => _messages.Add(error);
    public void AddMessages(IEnumerable<string> errors) => _messages.AddRange(errors);
    public void ClearMessages() => _messages.Clear();

}
