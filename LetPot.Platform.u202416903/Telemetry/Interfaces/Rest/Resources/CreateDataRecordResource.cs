namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;

public record CreateDataRecordResource(string potMacAddress,
    string operationMode, double targetHumidityLevel, double currentHumidityLevel, 
    string operationPhase, DateTime emittedAt);
