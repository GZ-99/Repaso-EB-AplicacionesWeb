using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;

namespace LetPot.Platform.u202416903.Allocation.Domain.Model.Commands;

public record UpdatePotCommand(int Id,
    MacAddress macAddress, int customerId, double preferredHumidityLevel);
