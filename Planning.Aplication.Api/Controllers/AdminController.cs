using Microsoft.AspNetCore.Mvc;
using Planning.Application.Api.Dto;
using Planning.Domain.Core.Services;

namespace Planning.Application.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController(BasicViewService service) : ControllerBase
{
    [HttpPost("CreateRegister")]
    public void CreateRegister([FromBody] RegisterDto register) => service.CreateRegister([register.MapToDomain()]);

    [HttpPut("DeactiveRegister")]
    public void DeactiveRegister([FromBody] Guid id) => service.Deactivate(id);
    [HttpPut("ActivateRegister")]
    public void ActivateRegister([FromBody] Guid id) => service.Activate(id);
    [HttpPut("Paid")]
    public void Paid([FromBody] Guid id) => service.Paid(id);
    [HttpPut("UpdateValue")]
    public void UpdateValue([FromBody] Guid id, decimal referenceValue) => service.UpdateValue(id, referenceValue);

    [HttpPut("UpdateBase")]
    public async Task UpdateBase() => await service.UpdateFile();

}
