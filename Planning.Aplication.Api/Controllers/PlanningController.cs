using Microsoft.AspNetCore.Mvc;
using Planning.Domain.Core.Model;
using Planning.Domain.Core.Services;

namespace Planning.Application.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanningController(BasicViewService service) : ControllerBase
{
    [HttpGet("GetBills")]
    public IReadOnlyList<Register> GetBills() => service.ViewData().GetBills();

    [HttpGet("GetReceivables")]
    public IReadOnlyList<Register> GetReceivables() => service.ViewData().GetReceivables();

    [HttpGet("GetReceived")]
    public IReadOnlyList<Register> GetReceived() => service.ViewData().GetReceived();

    [HttpGet("GetAssets")]
    public IReadOnlyList<Register> GetAssets() => service.ViewData().GetAssets();

}
