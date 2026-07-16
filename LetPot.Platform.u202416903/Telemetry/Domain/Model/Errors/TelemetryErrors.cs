using LetPot.Platform.u202416903.Shared.Domain.Model;

namespace LetPot.Platform.u202416903.Telemetry.Domain.Model.Errors;

public static class TelemetryErrors
{
    public static readonly Error DataRecordCreationFailed =
        new("Telemetry.DataRecordCreationFailed", "An error occurred while creating the DataRecord.");

    public static readonly Error DataRecordNotFound =
        new("Telemetry.DataRecordNotFound", "The specified DataRecord was not found.");
}
