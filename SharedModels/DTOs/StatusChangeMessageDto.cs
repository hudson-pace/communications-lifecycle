using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.DTOs;

public class StatusChangeMessageDto
{
  public int CommunicationId { get; set; }
  public int CommunicationStatusId { get; set; }
}
