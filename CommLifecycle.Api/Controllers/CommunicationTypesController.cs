using Microsoft.AspNetCore.Mvc;
using CommLifecycle.Api.Services;
using SharedModels.DTOs;
using SharedModels.Models;
using CommLifecycle.Api.Services.CommunicationTypes;

namespace CommLifecycle.Api.Controllers;

[ApiController]
[Route("CommunicationTypes")]
public class CommunicationTypesController : ControllerBase
{
  private readonly ICommunicationTypeService _communicationTypeService;

  public CommunicationTypesController(ICommunicationTypeService communicationTypeService)
  {
    _communicationTypeService = communicationTypeService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll() => (await _communicationTypeService.GetAllAsync(HttpContext.RequestAborted)).ToActionResult();
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetOne(int id) => (await _communicationTypeService.GetByIdAsync(id, HttpContext.RequestAborted)).ToActionResult();
  [HttpPost]
  public async Task<IActionResult> Create(CommunicationTypeDto communicationTypeDto) =>
    (await _communicationTypeService.CreateAsync(communicationTypeDto, HttpContext.RequestAborted))
    .ToActionResult();
  [HttpPut("{id:int}")]
  public async Task<IActionResult> Update(int id, CommunicationTypeDto communicationTypeDto) =>
    (await _communicationTypeService.UpdateAsync(id, communicationTypeDto, HttpContext.RequestAborted))
    .ToActionResult();
}