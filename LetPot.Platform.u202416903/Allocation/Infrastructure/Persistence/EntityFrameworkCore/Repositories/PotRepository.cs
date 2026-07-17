using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LetPot.Platform.u202416903.Allocation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PotRepository(AppDbContext context) : BaseRepository<Pot>(context), IPotRepository
{
    public async Task<bool> ExistsByMacAddressAsync(string macAddress, CancellationToken cancellationToken)
    {
        return await Context.Set<Pot>().AnyAsync(pot => pot.macAddress == new MacAddress(macAddress), cancellationToken);
    }
    
    public async Task<Pot?> FindByMacAddressAsync(string macAddress, CancellationToken cancellationToken)
    {
        return await Context.Set<Pot>()
            .FirstOrDefaultAsync(
                pot => pot.macAddress == new MacAddress(macAddress), cancellationToken);
    }
}
