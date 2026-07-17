namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Acl;

public interface IPotContextFacade
{
    Task<bool> ExistsByMacAddressAsync(string macAddress, CancellationToken cancellationToken);
}
