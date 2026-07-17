using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Acl;

namespace LetPot.Platform.u202416903.Telemetry.Application.Acl;

public class PotContextFacade(
    IPotRepository potRepository
) : IPotContextFacade
{
    public async Task<bool> ExistsByMacAddressAsync(
        string macAddress, 
        CancellationToken cancellationToken)
    {
        return await potRepository.ExistsByMacAddressAsync(macAddress, cancellationToken);
    }
}
