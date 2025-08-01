using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Services;
using SharedModels.DTOs;
using SharedModels.Models;

namespace MoviesApi.Controllers;

[ApiController]
[Route("communications")]
public class CommunicationsController : ControllerBase
{
  private readonly ICommunicationService _communicationService;
  private readonly ILogger<CommunicationsController> _logger;

  public CommunicationsController(ICommunicationService communicationService, ILogger<CommunicationsController> logger)
  {
    _communicationService = communicationService;
    _logger = logger;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    List<CommunicationDto> communications = await _communicationService.GetAllCommunicationsAsync();
    return communications is null ? NotFound() : Ok(communications);
  }
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetOne(int id)
  {
    CommunicationDto? communication = await _communicationService.GetCommunicationAsync(id);
    return communication is null ? NotFound() : Ok(communication);
  }
  [HttpPost]
  public async Task<IActionResult> Create(CommunicationDto communicationDto)
  {
    Communication communication = await _communicationService.CreateCommunicationAsync(communicationDto);
    return communication is null ? BadRequest() : NoContent();
  }
}