using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using LetPot.Platform.u202416903.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Telemetry.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LetPot.Platform.u202416903.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DataRecordRepository(AppDbContext context) : BaseRepository<DataRecord>(context), IDataRecordRepository
{
    public async Task<IEnumerable<DataRecord>> FindByPotMacAddressAsync(MacAddress potMacAddress, CancellationToken cancellationToken)
    {
        return await Context.Set<DataRecord>()
            .Where(dataRecord => dataRecord.potMacAddress == potMacAddress)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<bool> ExistsByPotMacAddressAsync(MacAddress potMacAddress, CancellationToken cancellationToken)
    {
        return await Context.Set<DataRecord>().AnyAsync(dataRecord => dataRecord.potMacAddress == potMacAddress, cancellationToken);
    }
    
    public new async Task<DataRecord?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<DataRecord>()
            .FirstOrDefaultAsync(dataRecord => dataRecord.Id == id, cancellationToken);
    }
    
    public new async Task<IEnumerable<DataRecord>> ListAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<DataRecord>()
            .ToListAsync(cancellationToken);
    }
}
