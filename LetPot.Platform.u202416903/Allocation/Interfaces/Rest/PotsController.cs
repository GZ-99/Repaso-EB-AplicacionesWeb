using System.Net.Mime;
using LetPot.Platform.u202416903.Allocation.Application.QueryServices;
using LetPot.Platform.u202416903.Allocation.Domain.Model.Queries;
using LetPot.Platform.u202416903.Allocation.Interfaces.Rest.Resources;
using LetPot.Platform.u202416903.Allocation.Interfaces.Rest.Transform;
using LetPot.Platform.u202416903.Shared.Interfaces.Rest.ProblemDetails;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace LetPot.Platform.u202416903.Allocation.Interfaces.Rest;

[ApiController]
[Route("api/v1/pots")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Pots endpoints")]
public class PotsController(
    IPotQueryService potQueryService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all pots",
        Description = "Get all pots",
        OperationId = "GetAllPots")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of pots", typeof(IEnumerable<PotResource>))]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var getAllPotsQuery = new GetAllPotsQuery();
        var pots = await potQueryService.Handle(getAllPotsQuery, cancellationToken);
        var potResources = pots.Select(PotResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(potResources);
    }
}
