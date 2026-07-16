using LetPot.Platform.u202416903.Telemetry.Application.QueryServices;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Queries;
using LetPot.Platform.u202416903.Telemetry.Domain.Repositories;

namespace LetPot.Platform.u202416903.Telemetry.Application.Internal.QueryServices;

public class DataRecordQueryService(IDataRecordRepository dataRecordRepository) : IDataRecordQueryService
{
    public async Task<DataRecord?> Handle(GetDataRecordByIdQuery query, CancellationToken cancellationToken)
    {
        return await dataRecordRepository.FindByIdAsync(query.dataRecordId, cancellationToken);
    }
}
