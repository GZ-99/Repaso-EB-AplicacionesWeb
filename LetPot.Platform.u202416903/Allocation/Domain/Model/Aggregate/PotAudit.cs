using LetPot.Platform.u202416903.Shared.Domain.Model.Entities;

namespace LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;

public partial class Pot : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
