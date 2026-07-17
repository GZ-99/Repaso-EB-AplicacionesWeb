using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;

namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Transform;

public static class DataRecordResourceFromEntityAssembler
{
    public static DataRecordResource ToResourceFromEntity(DataRecord entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "DataRecord entity cannot be null when converting to resource.");

        return new DataRecordResource(
            entity.Id,
            entity.potMacAddress.value,
            entity.operationMode.ToString(),
            entity.targetHumidityLevel,
            entity.currentHumidityLevel,
            entity.operationPhase.ToString(),
            entity.emittedAt);
    }
}
