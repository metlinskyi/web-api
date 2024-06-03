namespace abc.Infrastructure.Tasks;
public enum TaskStatus : byte {
    Unknown, Enqueued, Process, Completed, Faulted
}
