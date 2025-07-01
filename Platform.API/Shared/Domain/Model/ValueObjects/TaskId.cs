namespace Platform.API.Shared.Domain.Repositories.Model.ValueObjects;

public class TaskId
{
    public long taskId { get; private set; }

    public TaskId(long value)
    {
        taskId = value;
    }

    public TaskId() : this(0L) { }

    public override string ToString() => taskId.ToString();
}