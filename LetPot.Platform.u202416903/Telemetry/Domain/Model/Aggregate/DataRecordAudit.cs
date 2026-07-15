using LetPot.Platform.u202416903.Shared.Domain.Model.Entities;

namespace LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;

public partial class DataRecord : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
