namespace LetPot.Platform.u202416903.Telemetry.Interfaces.Acl;

public interface IPotContextFacade
{
    /*Task<int> CreatePot(
        string macAddress,
        int customerId,
        double preferredHumidityLevel,
        CancellationToken cancellationToken
    );*/
    
    Task<int> FetchPotById(string macAddress, 
        CancellationToken cancellationToken);
}
