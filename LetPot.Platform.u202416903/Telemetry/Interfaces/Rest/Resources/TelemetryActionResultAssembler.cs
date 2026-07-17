using LetPot.Platform.u202416903.Shared.Application.Model;
using LetPot.Platform.u202416903.Shared.Interfaces.Rest.ProblemDetails;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using LetPot.Platform.u202416903.Telemetry.Domain.Model;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;

public static class TelemetryActionResultAssembler
{
    private static int ToStatusCodeFromTelemetryError(TelemetryError error)
    {
        return error switch
        {
            TelemetryError.DataRecordNotFound => StatusCodes.Status404NotFound,
            TelemetryError.PotNotFound => StatusCodes.Status404NotFound,
            TelemetryError.DuplicatePotMacAddressTitle => StatusCodes.Status409Conflict,
            TelemetryError.OperationCancelled => StatusCodes.Status409Conflict,
            TelemetryError.DatabaseError => StatusCodes.Status500InternalServerError,
            TelemetryError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest
        };
    }
    
    public static IActionResult ToActionResultFromCreateDataRecordResult(
        ControllerBase controller,
        Result<DataRecord> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<DataRecord, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var statusCode = ToStatusCodeFromTelemetryError((TelemetryError)result.Error!);
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, result.Message);
    }
}
