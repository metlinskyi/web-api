using abc.Infrastructure.Controllers;
using abc.Infrastructure.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace abc.Endpoints.Country;

[ApiRoute]
[ApiController]
public class CountryController : TaskController<string, CountryModel>
{
    public CountryController(
        ILogger<CountryController> logger,
        ITaskList tasks) : base(logger, tasks)
    {
    }
    [HttpGet()]
    public IActionResult Get([FromQuery] string countryId) => RequestHandler(countryId.ToUpper());
    protected override ITask CreateTask(string request) => new CountryTask(request);
    protected override string CreateKey(string request) => request.ToString();
}