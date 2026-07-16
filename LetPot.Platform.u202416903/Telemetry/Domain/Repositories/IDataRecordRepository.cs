using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;

namespace LetPot.Platform.u202416903.Telemetry.Domain.Repositories;

public interface IDataRecordRepository : IBaseRepository<DataRecord>
{
    Task<IEnumerable<DataRecord>> FindByPotMacAddressAsync(MacAddress potMacAddress, CancellationToken cancellationToken);

    Task<bool> ExistsByPotMacAddressAsync(MacAddress potMacAddress, CancellationToken cancellationToken);
}
