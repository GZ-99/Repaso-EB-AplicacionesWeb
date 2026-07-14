using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;

namespace LetPot.Platform.u202416903.Allocation.Domain.Repositories;

public interface IPotRepository : IBaseRepository<Pot>
{
    Task<bool> ExistsByMacAddressAsync(string macAddress, CancellationToken cancellationToken);
}
