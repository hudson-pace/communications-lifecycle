using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CommLifecycle.Api.Services;
using SharedModels.DTOs;
using SharedModels.Models;

namespace CommLifecycle.Api.Controllers;

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
  public async Task<IActionResult> GetAll() => (await _communicationService.GetAllAsync(HttpContext.RequestAborted)).ToActionResult();
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetOne(int id) => (await _communicationService.GetByIdAsync(id, HttpContext.RequestAborted)).ToActionResult();
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CommunicationDto communicationDto) => (await _communicationService.CreateAsync(communicationDto, HttpContext.RequestAborted)).ToActionResult();
}