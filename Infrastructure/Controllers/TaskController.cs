using abc.Infrastructure.Models;
using abc.Infrastructure.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace abc.Infrastructure.Controllers;

public abstract class TaskController<TIn, TOut> : ControllerBase
    where TOut : IModel
{
    private readonly ITaskList tasks;
    protected ILogger<TaskController<TIn, TOut>> Logger { get;} 
    protected TaskController(ILogger<TaskController<TIn, TOut>> logger, ITaskList tasks)
    {
        Logger = logger;
        this.tasks = tasks;
    }

    protected IActionResult RequestHandler(TIn request)
    {
        var key = CreateKey(request);
        var task = tasks.GetOrAdd(key, () => CreateTask(request));
        switch(task.Status){
            case Tasks.TaskStatus.Enqueued:
            case Tasks.TaskStatus.Process:
                return StatusCode(201, StateUrl(key));   
            case Tasks.TaskStatus.Completed:
                return GetTaskResult(task);
            case Tasks.TaskStatus.Faulted:
                return StatusCode(500, task.Error?.Message);  
        }
        return  BadRequest();  
    }

    [HttpGet("State/{key}")]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public IActionResult RequestState(string key)
    {
        var task = tasks.Get(key);
        switch(task.Status){
            case Tasks.TaskStatus.Enqueued:
            case Tasks.TaskStatus.Process:
                return StatusCode(102);  
            case Tasks.TaskStatus.Completed:
                return GetTaskResult(task);
            case Tasks.TaskStatus.Faulted:
                return StatusCode(500, task.Error?.Message);  
        }
        return  BadRequest();  
    }

    protected virtual TOut ResponseHandler(TOut result) => result;

    protected abstract ITask CreateTask(TIn request);

    protected abstract string CreateKey(TIn request);

    private IActionResult GetTaskResult(ITask task)
    {
        var taskOf = task as ITask<TOut>;
        return taskOf == null 
            ? BadRequest() 
            : Ok(ResponseHandler(taskOf.Result));
    }

    private string StateUrl(string key)
    {
        var ad = ControllerContext.ActionDescriptor;
        return $"/{ApiRouteAttribute.Prefix}/{ad.ControllerName}/State/{key}";
    }
}
