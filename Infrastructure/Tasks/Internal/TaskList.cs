using System.Collections.Concurrent;
using abc.Infrastructure.Tasks;

internal class TaskList : ITaskList, IDisposable
{
    private readonly ConcurrentDictionary<string, ITask> tasks = new();
    private readonly TaskFactory factory;
    
    public TaskList()
    {
       var cts = new CancellationTokenSource();
       factory = new TaskFactory(cts.Token);
    }

    public void Dispose()
    {
    }

    public ITask Get(string key)
    {
        return tasks.TryGetValue(key, out var task) 
            ? task
            : throw new KeyNotFoundException(key);
    }

    public ITask GetAndRemove(string key)
    {
        return tasks.TryRemove(key, out var task) 
            ? task 
            : throw new KeyNotFoundException(key);
    }

    public ITask GetOrAdd(string key, Func<ITask> activator)
    {
        return tasks.AddOrUpdate(key, 
        (k) => 
        { 
                var task = activator();
                task.Status = abc.Infrastructure.Tasks.TaskStatus.Enqueued;
                factory.StartNew(() => task.Run());
                return task;  
        },
        (k, task) => 
        { 
                return task;  
        });
    }
}