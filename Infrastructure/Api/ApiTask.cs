using abc.Infrastructure.Tasks;

internal abstract class ApiTask<TModel> : ITask<TModel>
{
    public abc.Infrastructure.Tasks.TaskStatus Status { get; set;}
    public Exception? Error { get; set;}
    public TModel? Result { get; protected set;}

    public void Run()
    {
        try
        {
            Status = abc.Infrastructure.Tasks.TaskStatus.Process;
            Action();
            Status = abc.Infrastructure.Tasks.TaskStatus.Completed;
        }
        catch(Exception e)
        {
            Status = abc.Infrastructure.Tasks.TaskStatus.Faulted;
            Error = e;
        }
    }

    protected abstract void Action();
}