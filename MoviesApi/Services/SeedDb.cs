using SharedModels.Models;

namespace MoviesApi.Services;

public class SeedDb
{
    public SeedDb()
    { }
    public async Task Seed(MoviesApiContext context)
    {
        CommunicationType EobType = new()
        {
            Name = "EOB",
            Statuses = [
                new CommunicationStatus {
                    Description = "Released"
                },
                new CommunicationStatus {
                    Description = "Printed"
                }]
        };
        CommunicationType EopType = new()
        {
            Name = "EOP",
            Statuses = [
                new CommunicationStatus {
                    Description = "Released"
                },
                new CommunicationStatus {
                    Description = "Delivered"
                }],
        };
        CommunicationType IdCardType = new()
        {
            Name = "ID Card",
            Statuses = [
                new CommunicationStatus {
                    Description = "QueuedForPrinting"
                },
                new CommunicationStatus {
                    Description = "Printed"
                }],
        };

        context.CommunicationTypes.Add(EobType);
        context.CommunicationTypes.Add(EopType);
        context.CommunicationTypes.Add(IdCardType);
        await context.SaveChangesAsync();
        

        Communication Eob1 = new()
        {
            Title = "Eob 1",
            Type = EobType,
            StatusHistory = [..EobType.Statuses.Select(status => new CommunicationStatusChange
                {
                    Status = status,
                })],
        };
        Communication Eop1 = new()
        {
            Title = "Eop 1",
            Type = EopType,
            StatusHistory = [..EopType.Statuses.Select(status => new CommunicationStatusChange
                {
                    Status = status,
                })]
        };
        Communication idCard1 = new()
        {
            Title = "ID Card 1",
            Type = IdCardType,
            StatusHistory = [..IdCardType.Statuses.Select(status => new CommunicationStatusChange
                {
                    Status = status,
                })]
        };

        context.Communications.Add(Eob1);
        context.Communications.Add(Eop1);
        context.Communications.Add(idCard1);
        await context.SaveChangesAsync();
    }
}