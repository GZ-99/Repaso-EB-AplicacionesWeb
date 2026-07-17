using LetPot.Platform.u202416903.Shared.Application.Model;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using LetPot.Platform.u202416903.Telemetry.Application.CommandServices;
using LetPot.Platform.u202416903.Telemetry.Domain.Model;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Commands;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Events;
using LetPot.Platform.u202416903.Telemetry.Domain.Repositories;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Acl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace LetPot.Platform.u202416903.Telemetry.Application.Internal.CommandServices;

public class DataRecordCommandService(
    IDataRecordRepository dataRecordRepository,
    IPotContextFacade potContextFacade,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer
    ) : IDataRecordCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;
    
    public async Task<Result<DataRecord>> Handle(CreateDataRecordCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await potContextFacade.ExistsByMacAddressAsync(command.potMacAddress,
                cancellationToken);
            if (!exists)
            {
                return Result<DataRecord>.Failure(
                    TelemetryError.PotNotFound,
                    _localizer[nameof(TelemetryError.PotNotFound)]);
            }
            
            var dataRecord = new DataRecord(command);
            
            await dataRecordRepository.AddAsync(dataRecord, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            
            /*var domainEvent = new DataRecordRegisteredEvent(
                dataRecord.potMacAddress,
                dataRecord.targetHumidityLevel);*/
            
            return Result<DataRecord>.Success(dataRecord);
        }
        catch (OperationCanceledException)
        {
            return Result<DataRecord>.Failure(TelemetryError.OperationCancelled, 
                _localizer[nameof(TelemetryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<DataRecord>.Failure(TelemetryError.DatabaseError,
                _localizer[nameof(TelemetryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<DataRecord>.Failure(TelemetryError.InternalServerError,
                _localizer[nameof(TelemetryError.InternalServerError)]);
        }
    }
}
