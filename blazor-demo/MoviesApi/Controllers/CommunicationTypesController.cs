using Microsoft.AspNetCore.Mvc;
using MoviesApi.Services;
using SharedModels.DTOs;
using SharedModels.Models;

[ApiController]
[Route("CommunicationTypes")]
public class CommunicationTypesController : ControllerBase
{
  private readonly ICommunicationService _communicationService;

  public CommunicationTypesController(ICommunicationService communicationService)
  {
    _communicationService = communicationService;
  }

  [HttpGet("/")]
  public async Task<IActionResult> GetAll()
  {
    List<CommunicationTypeDto> communicationTypes = await _communicationService.GetAllCommunicationTypesAsync();
    return communicationTypes is null ? NotFound() : Ok(communicationTypes);
  }
  [HttpGet("/{id:int}")]
  public async Task<IActionResult> GetOne(int id)
  {
    CommunicationTypeDto? communicationType = await _communicationService.GetCommunicationTypeAsync(id);
    return communicationType is null ? NotFound() : Ok(communicationType);
  }
  [HttpPost("/")]
  public async Task<IActionResult> Create(CommunicationTypeDto communicationTypeDto)
  {
    CommunicationType communicationType = await _communicationService.CreateCommunicationTypeAsync(communicationTypeDto);
    return communicationType is null ? BadRequest() : NoContent();
  }
  [HttpPut("/{id:int}")]
  public async Task<IActionResult> Update(int id, CommunicationTypeDto communicationTypeDto)
  {
    CommunicationType communicationType = await _communicationService.UpdateCommunicationTypeAsync(communicationTypeDto);
    return communicationType is null ? BadRequest() : NoContent();
  }
}