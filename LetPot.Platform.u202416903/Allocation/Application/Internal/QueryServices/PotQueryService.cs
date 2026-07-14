using LetPot.Platform.u202416903.Allocation.Application.QueryServices;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Queries;
using LetPot.Platform.u202416903.Allocation.Domain.Repositories;

namespace LetPot.Platform.u202416903.Allocation.Application.Internal.QueryServices;

public class PotQueryService(IPotRepository potRepository) : IPotQueryService
{
    public async Task<Pot?> Handle(GetPotByIdQuery query, CancellationToken cancellationToken)
    {
        return await potRepository.FindByIdAsync(query.id, cancellationToken);
    }
    
    public async Task<IEnumerable<Pot>> Handle(GetAllPotsQuery query, CancellationToken cancellationToken)
    {
        return await potRepository.ListAsync(cancellationToken);
    }
}
