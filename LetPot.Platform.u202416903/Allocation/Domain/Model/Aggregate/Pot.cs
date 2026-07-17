using LetPot.Platform.u202416903.Allocation.Domain.Model.Commands;
using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;

namespace LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;

public partial class Pot
{
    public Pot(string macAddress, int customerId, double preferredHumidityLevel) : this()
    {
        this.macAddress = new MacAddress(macAddress);
        this.customerId = customerId;
        this.preferredHumidityLevel = preferredHumidityLevel;
    }
    
    public Pot()
    {
        macAddress = new MacAddress(string.Empty);
        customerId = 0;
        preferredHumidityLevel = 0.0;
    }
    
    public Pot(SeedPotsCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
    }
    
    public Pot(UpdatePotCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        macAddress = command.macAddress;
        customerId = command.customerId;
        preferredHumidityLevel = command.preferredHumidityLevel;
    }
    
    public void UpdatePreferredHumidityLevel(double preferredHumidityLevel)
    {
        this.preferredHumidityLevel = preferredHumidityLevel;
    }
    
    public int Id { get; }
    
    public MacAddress macAddress { get; set; }
    public int customerId { get; set; }
    public double preferredHumidityLevel { get; set; }
}
