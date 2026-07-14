using LetPot.Platform.u202416903.Allocation.Domain.Model;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using LetPot.Platform.u202416903.Shared.Interfaces.Rest.ProblemDetails;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LetPot.Platform.u202416903.Allocation.Interfaces.Rest.Transform;

public class AllocationActionResultAssembler
{
    private static int ToStatusCodeFromAllocationError(AllocationError error)
    {
        return error switch
        {
            AllocationError.PotNotFound => StatusCodes.Status404NotFound,
            AllocationError.OperationCancelled => StatusCodes.Status409Conflict,
            AllocationError.DatabaseError => StatusCodes.Status500InternalServerError,
            AllocationError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }
    
    public static IActionResult ToActionResultFromGetPotByIdResult(
        ControllerBase controller,
        Pot? pot,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<Pot, IActionResult> successAction)
    {
        if (pot is null)
            return problemDetailsFactory.CreateProblemDetails(
                controller,
                ToStatusCodeFromAllocationError(AllocationError.PotNotFound),
                AllocationError.PotNotFound,
                errorLocalizer[nameof(AllocationError.PotNotFound)]
            );
        return successAction(pot);
    }
}