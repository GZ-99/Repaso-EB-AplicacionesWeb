using System.Net.Mime;
using LetPot.Platform.u202416903.Shared.Interfaces.Rest.ProblemDetails;
using LetPot.Platform.u202416903.Shared.Resources.Errors;
using LetPot.Platform.u202416903.Telemetry.Application.CommandServices;
using LetPot.Platform.u202416903.Telemetry.Application.QueryServices;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Queries;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Resources;
using LetPot.Platform.u202416903.Telemetry.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Rest;

[ApiController]
[Route("/api/v1/data-records")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Data Record endpoints")]
public class DataRecordsController(
        IDataRecordQueryService dataRecordQueryService,
        IDataRecordCommandService dataRecordCommandService,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory)
        : ControllerBase
{
        private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
        private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
        
        [HttpPost]
        [SwaggerOperation(
                Summary = "Create a DataRecord",
                Description = "Create a DataRecord",
                OperationId = "CreateDataRecord")]
        [SwaggerResponse(StatusCodes.Status201Created, "The DataRecord was created", typeof(DataRecordResource))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The DataRecord was not created")]
        public async Task<IActionResult> CreateDataRecord([FromBody] CreateDataRecordResource resource,
                CancellationToken cancellationToken)
        {
                var createDataRecordCommand = CreateDataRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await dataRecordCommandService.Handle(createDataRecordCommand, cancellationToken);

                return TelemetryActionResultAssembler.ToActionResultFromCreateDataRecordResult(
                        this,
                        result,
                        _errorLocalizer,
                        _problemDetailsFactory,
                        createdDataRecord => CreatedAtAction(nameof(GetDataRecordByIdQuery), new { dataRecordId = createdDataRecord.Id },
                                DataRecordResourceFromEntityAssembler.ToResourceFromEntity(createdDataRecord))
                );
        }
}
