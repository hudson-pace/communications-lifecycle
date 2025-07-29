using SharedModels.Models;

namespace MoviesApi.Services;

public class SeedDb
{
  public SeedDb()
  {}
  public async Task Seed(MoviesApiContext context)
  {
    CommunicationType EobType = new() { Name = "EOB" };
    CommunicationType EopType = new() { Name = "EOP" };
    CommunicationType IdCardType = new() { Name = "ID Card" };

    context.CommunicationTypes.Add(EobType);
    context.CommunicationTypes.Add(EopType);
    context.CommunicationTypes.Add(IdCardType);
    await context.SaveChangesAsync();

    Communication Eob1 = new()
    {
      Title = "Eob 1",
      Type = EobType,
      StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EobType,
                    Description = "Released",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EobType,
                    Description = "Printed",
                }
            }
        ],
    };
    Communication Eop1 = new()
    {
      Title = "Eop 1",
      Type = EopType,
      StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EopType,
                    Description = "Released",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = EopType,
                    Description = "Delivered",
                }
            }
        ],
    };
    Communication IdCard1 = new()
    {
      Title = "ID Card 1",
      Type = IdCardType,
      StatusHistory = [
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = IdCardType,
                    Description = "QueuedForPrinting",
                }
            },
            new CommunicationStatusChange {
                Status = new CommunicationStatus {
                    Type = IdCardType,
                    Description = "Printed",
                }
            }
        ],
    };

    context.Communications.Add(Eob1);
    context.Communications.Add(Eop1);
    context.Communications.Add(IdCard1);
    await context.SaveChangesAsync();

    Console.WriteLine("More Seed to Sow");
  }
}