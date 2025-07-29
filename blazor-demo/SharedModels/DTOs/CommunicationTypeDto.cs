using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTOs;

public class CommunicationTypeDto
{
  public int Id { get; set; }

  public string Name { get; set; } = null!;
}
