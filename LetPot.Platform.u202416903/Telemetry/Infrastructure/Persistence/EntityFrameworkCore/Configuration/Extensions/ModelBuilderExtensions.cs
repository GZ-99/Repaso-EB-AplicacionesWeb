using LetPot.Platform.u202416903.Shared.Domain.Model.ValueObjects;
using LetPot.Platform.u202416903.Telemetry.Domain.Model.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace LetPot.Platform.u202416903.Telemetry.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTelemetryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<DataRecord>().HasKey(t => t.Id);
        builder.Entity<DataRecord>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DataRecord>().Property(t => t.potMacAddress)
            .HasConversion(
                v => v.value,
                v => new MacAddress(v))
            .IsRequired();
        builder.Entity<DataRecord>().Property(t => t.operationMode).IsRequired();
        builder.Entity<DataRecord>().Property(t => t.targetHumidityLevel).IsRequired();
        builder.Entity<DataRecord>().Property(t => t.currentHumidityLevel).IsRequired();
        builder.Entity<DataRecord>().Property(t => t.operationPhase).IsRequired();
        builder.Entity<DataRecord>().Property(t => t.emittedAt).IsRequired();
    }
}
