using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Acl;

namespace LetPot.Platform.u202416903.Telemetry.Application.Acl;

public class PotContextFacade(
    IPotRepository potRepository,
    IUnitOfWork unitOfWork
) : IPotContextFacade
{
    public async Task<bool> ExistsByMacAddressAsync(
        string macAddress, 
        CancellationToken cancellationToken)
    {
        return await potRepository.ExistsByMacAddressAsync(macAddress, cancellationToken);
    }

    public async Task UpdatePreferredHumidityLevelAsync(string macAddress, double preferredHumidityLevel,
        CancellationToken cancellationToken)
    {
        var pot = await potRepository.FindByMacAddressAsync(macAddress, cancellationToken);

        if (pot is null)
            return;
        
        pot.UpdatePreferredHumidityLevel(preferredHumidityLevel);
        
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
