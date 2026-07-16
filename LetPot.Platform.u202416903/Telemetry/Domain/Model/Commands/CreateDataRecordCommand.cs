namespace LetPot.Platform.u202416903.Telemetry.Domain.Model.Commands;

public record CreateDataRecordCommand(string potMacAddress,
    string operationMode, double targetHumidityLevel, double currentHumidityLevel, 
    string operationPhase, DateTime emittedAt);
    