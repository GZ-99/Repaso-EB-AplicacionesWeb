using LetPot.Platform.u202416903.Telemetry.Domain.Model.Commands;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;

namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Transform;

public static class CreateDataRecordCommandFromResourceAssembler
{
    public static CreateDataRecordCommand ToCommandFromResource(CreateDataRecordResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateDataRecordResource cannot be null when converting to command.");
        return new CreateDataRecordCommand(
            resource.potMacAddress,
            resource.operationMode,
            resource.targetHumidityLevel,
            resource.currentHumidityLevel,
            resource.operationPhase,
            resource.emittedAt);
    }
}
