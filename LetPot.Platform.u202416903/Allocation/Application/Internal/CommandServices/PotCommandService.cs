using LetPot.Platform.u202416903.Allocation.Application.CommandServices;
using LetPot.Platform.u202416903.Allocation.Domain.Model;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Commands;
using LetPot.Platform.u202416903.Allocation.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Application.Model;
using LetPot.Platform.u202416903.Shared.Domain.Repositories;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace LetPot.Platform.u202416903.Allocation.Application.Internal.CommandServices;

public class PotCommandService(
    IPotRepository potRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    ILogger<PotCommandService> logger) : IPotCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;
    
    public async Task<Result<Pot>> Handle(UpdatePotCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var pot = await potRepository.FindByIdAsync(command.Id,  cancellationToken);
            if (pot == null)
                return Result<Pot>.Failure(AllocationError.PotNotFound,
                    _localizer[nameof(AllocationError.PotNotFound)]);
            
            pot.macAddress = command.macAddress;
            pot.customerId = command.customerId;
            pot.preferredHumidityLevel = command.preferredHumidityLevel;

            potRepository.Update(pot);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<Pot>.Success(pot);
        }
        catch (OperationCanceledException)
        {
            return Result<Pot>.Failure(AllocationError.OperationCancelled,
                _localizer[nameof(AllocationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Pot>.Failure(AllocationError.DatabaseError,
                _localizer[nameof(AllocationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Pot>.Failure(AllocationError.InternalServerError,
                _localizer[nameof(AllocationError.InternalServerError)]);
        }
    }
    
    public async Task<Result<Pot>> Handle(SeedPotsCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var seedPots = new List<Pot>
            {
                new Pot("67-E0-B5-2B-DB-67", 2, 40.0),
                new Pot("69-3D-91-E2-AA-DC", 4, 70.0),
                new Pot("37-AA-35-CE-E6-C2", 3, 45.5),
                new Pot("FA-8C-71-C2-C4-79", 1, 57.5)
            };
            foreach (var pot in seedPots)
            {
                var exists = await potRepository.ExistsByMacAddressAsync(
                    pot.macAddress.value,
                    cancellationToken
                );
                
                if (!exists)
                    await potRepository.AddAsync(pot, cancellationToken);
            }
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Pot>.Success(seedPots.First());
        }
        catch (OperationCanceledException)
        {
            return Result<Pot>.Failure(AllocationError.OperationCancelled,
                _localizer[nameof(AllocationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Pot>.Failure(AllocationError.DatabaseError,
                _localizer[nameof(AllocationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Pot>.Failure(AllocationError.InternalServerError,
                _localizer[nameof(AllocationError.InternalServerError)]);
        }
    }
}
