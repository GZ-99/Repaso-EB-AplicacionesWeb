using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Queries;

namespace LetPot.Platform.u202416903.Allocation.Application.QueryServices;

public interface IPotQueryService
{
    Task<Pot?> Handle(GetPotByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Pot>> Handle(GetAllPotsQuery query, CancellationToken cancellationToken);
}
