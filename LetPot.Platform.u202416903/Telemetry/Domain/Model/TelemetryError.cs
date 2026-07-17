namespace LetPot.Platform.u202416903.Telemetry.Domain.Model;

public enum TelemetryError
{
    None,
    DataRecordNotFound,
    PotNotFound,
    DuplicatePotMacAddressTitle,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
