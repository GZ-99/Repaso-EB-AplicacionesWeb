using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Commands;
using LetPot.Platform.u202416903.Shared.Application.Model;

namespace LetPot.Platform.u202416903.Allocation.Application.CommandServices;

public interface IPotCommandService
{
    Task<Result<Pot>> Handle(SeedPotsCommand command,  CancellationToken cancellationToken);
    Task<Result<Pot>> Handle(UpdatePotCommand command,  CancellationToken cancellationToken);
}
