namespace abc.Infrastructure.Tasks;

public interface ITask
{
    TaskStatus Status { get; set; }
    Exception? Error { get; set; }
    void Run();
}

public interface ITask<TResult> : ITask
{
    TResult? Result { get; }
}