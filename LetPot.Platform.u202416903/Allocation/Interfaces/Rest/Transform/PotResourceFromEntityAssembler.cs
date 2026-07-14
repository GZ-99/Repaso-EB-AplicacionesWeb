using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Interfaces.Rest.Resources;

namespace LetPot.Platform.u202416903.Allocation.Interfaces.Rest.Transform;

public class PotResourceFromEntityAssembler
{
    public static PotResource ToResourceFromEntity(Pot entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Pot entity cannot be null when converting to resource.");

        return new PotResource(
            entity.Id,
            entity.macAddress.value,
            entity.customerId,
            entity.preferredHumidityLevel);
    }
}
