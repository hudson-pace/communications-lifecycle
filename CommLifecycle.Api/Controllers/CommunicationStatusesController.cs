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
  public async Task<IActionResult> Create(int id, CommunicationStatusChangeDto communicationStatusChangeDto) =>
    (await _communicationService.AppendCommunicationStatusChangeAsync(id, communicationStatusChangeDto, HttpContext.RequestAborted))
    .ToActionResult();
}
