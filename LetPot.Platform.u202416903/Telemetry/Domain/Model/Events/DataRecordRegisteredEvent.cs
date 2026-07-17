using LetPot.Platform.u202416903.Shared.Domain.Model.Events;
using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;

namespace LetPot.Platform.u202416903.Telemetry.Domain.Model.Events;

public class DataRecordRegisteredEvent(MacAddress potMacAddress, double targetHumidityLevel) : IEvent
{
    public MacAddress PotMacAddress { get; } = potMacAddress;
    public double TargetHumidityLevel { get; } = targetHumidityLevel;
}
