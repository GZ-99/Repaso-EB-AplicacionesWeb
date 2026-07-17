namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;

public record DataRecordResource(int id, string potMacAddress,
    string operationMode, double targetHumidityLevel, double currentHumidityLevel, 
    string operationPhase, DateTime emittedAt);
    