using LetPot.Platform.u202416903.Allocation.Application.CommandServices;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Commands;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Events;
using LetPot.Platform.u202416903.Shared.Application.Internal.EventHandlers;

namespace LetPot.Platform.u202416903.Allocation.Application.Internal.EventHandlers;

public class ApplicationReadyEventHandler(IPotCommandService potCommandService) : IEventHandler<ApplicationReadyEvent>
{
    public Task Handle(ApplicationReadyEvent domainEvent, CancellationToken cancellationToken)
    {
        return On(domainEvent, cancellationToken);
    }
    
    private async Task On(ApplicationReadyEvent domainEvent, CancellationToken  cancellationToken)
    {
        var seedCommand = new SeedPotsCommand();
        await potCommandService.Handle(seedCommand, cancellationToken);
    }
}
