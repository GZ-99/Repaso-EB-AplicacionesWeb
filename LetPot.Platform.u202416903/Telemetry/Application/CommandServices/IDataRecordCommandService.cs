using LetPot.Platform.u202416903.Shared.Application.Model;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Commands;

namespace LetPot.Platform.u202416903.Telemetry.Application.CommandServices;

public interface IDataRecordCommandService
{
    Task<Result<DataRecord>> Handle(CreateDataRecordCommand command, CancellationToken cancellationToken);
}
