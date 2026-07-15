using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.ValueObjects;

namespace LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;

public partial class DataRecord
{
    public DataRecord(string potMacAddress, EOperationMode operationMode, double targetHumidityLevel,
        double currentHumidityLevel, EOperationPhase operationPhase, DateTime emittedAt)
    {
        this.potMacAddress = new MacAddress(potMacAddress);
        this.operationMode = operationMode;
        this.targetHumidityLevel = targetHumidityLevel;
        this.currentHumidityLevel = currentHumidityLevel;
        this.operationPhase = operationPhase;
        this.emittedAt = emittedAt;
    }
    
    public DataRecord()
    {
        potMacAddress = new MacAddress(string.Empty);
        operationMode = EOperationMode.STAND_BY;
        targetHumidityLevel = 0.0;
        currentHumidityLevel = 0.0;
        operationPhase = EOperationPhase.WAITING;
        emittedAt = DateTime.UtcNow;
    }
    
    public int Id { get; }
    
    public MacAddress potMacAddress { get; set; }
    public EOperationMode operationMode { get; set; }
    public double targetHumidityLevel { get; set; }
    public double currentHumidityLevel { get; set; }
    public EOperationPhase operationPhase { get; set; }
    public DateTime emittedAt { get; set; }
}
