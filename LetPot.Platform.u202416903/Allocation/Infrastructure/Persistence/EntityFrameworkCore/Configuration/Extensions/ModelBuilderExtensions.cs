using LetPot.Platform.u202416903.Allocation.Domain.Model.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace LetPot.Platform.u202416903.Allocation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAllocationConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Pot>().HasKey(t => t.Id);
        builder.Entity<Pot>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Pot>().Property(t => t.macAddress).IsRequired();
        builder.Entity<Pot>().Property(t => t.customerId).IsRequired();
        builder.Entity<Pot>().Property(t => t.preferredHumidityLevel).IsRequired();
    }
}
