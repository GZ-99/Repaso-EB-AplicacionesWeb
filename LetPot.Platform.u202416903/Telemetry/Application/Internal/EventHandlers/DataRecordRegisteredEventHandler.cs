using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Application.Internal.EventHandlers;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Events;

namespace LetPot.Platform.u202416903.Telemetry.Application.Internal.EventHandlers;

public class DataRecordRegisteredEventHandler(
        IPotRepository potRepository,
        IUnitOfWork unitOfWork) 
    : IEventHandler<DataRecordRegisteredEvent>
{
    public async Task Handle(DataRecordRegisteredEvent dataRecordRegisteredEvent, CancellationToken cancellationToken)
    {
        var pot = await potRepository.FindByMacAddressAsync(dataRecordRegisteredEvent.PotMacAddress.value,
            cancellationToken);
        if (pot is null)
        {
            return;
        }
        if (pot.preferredHumidityLevel == dataRecordRegisteredEvent.TargetHumidityLevel)
            return;
        pot.UpdatePreferredHumidityLevel(dataRecordRegisteredEvent.TargetHumidityLevel);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
