namespace abc.Infrastructure.Tasks;

public interface ITaskList {
    ITask Get(string key);
    ITask GetOrAdd(string key, Func<ITask> activator);
    ITask GetAndRemove(string key);
}