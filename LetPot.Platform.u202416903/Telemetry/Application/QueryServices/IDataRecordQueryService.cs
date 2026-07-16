using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Queries;

namespace LetPot.Platform.u202416903.Telemetry.Application.QueryServices;

public interface IDataRecordQueryService
{
    Task<DataRecord?> Handle(GetDataRecordByIdQuery query, CancellationToken cancellationToken);
}
