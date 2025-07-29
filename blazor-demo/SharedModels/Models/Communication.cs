using System.ComponentModel.DataAnnotations;

namespace SharedModels.Models;

public class Communication
{
  public int Id { get; set; }

  [Required(ErrorMessage = "Title is required.")] 
  public string Title { get; set; } = null!;

  [Required(ErrorMessage = "Type is required.")]
  public CommunicationType Type { get; set; } = null!;

  public ICollection<CommunicationStatusChange> StatusHistory { get; set; } = new List<CommunicationStatusChange>();
}
