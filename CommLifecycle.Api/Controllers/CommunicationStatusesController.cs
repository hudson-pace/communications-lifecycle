using Microsoft.AspNetCore.Mvc;
using CommLifecycle.Api.Services;
using SharedModels.DTOs;
using SharedModels.Models;

namespace CommLifecycle.Api.Controllers;

[ApiController]
[Route("communications/{id:int}/StatusHistory")]
public class CommunicationStatusesController : ControllerBase
{
  private readonly ICommunicationService _communicationService;

  public CommunicationStatusesController(ICommunicationService communicationService)
  {
    _communicationService = communicationService;
  }

  [HttpPost]
  public async Task<IActionResult> Create(CommunicationStatusChangeDto communicationStatusChangeDto)
  {
    CommunicationStatusChange communicationStatusChange = await _communicationService.CreateCommunicationStatusChangeAsync(communicationStatusChangeDto);
    return communicationStatusChange is null ? BadRequest() : NoContent();
  }
}
