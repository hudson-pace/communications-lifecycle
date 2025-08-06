using SharedModels.DTOs;
namespace CommLifecycle.Api.Services.CommunicationTypes;

public interface ICommunicationTypeService
{
    Task<Result<List<CommunicationTypeDto>>> GetAllAsync(CancellationToken ct);
    Task<Result<CommunicationTypeDto>> GetByIdAsync(int id, CancellationToken ct);
    Task<Result<CommunicationTypeDto>> CreateAsync(CommunicationTypeDto dto, CancellationToken ct);
    Task<Result<CommunicationTypeDto>> UpdateAsync(int id, CommunicationTypeDto dto, CancellationToken ct);
    Task<Result<CommunicationTypeDto>> DeleteAsync(int id, CancellationToken ct);
}