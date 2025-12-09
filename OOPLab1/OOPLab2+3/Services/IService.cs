using OopMonitoringLab.Models;


namespace OopMonitoringLab.Services
{
    public interface IService
    {
        string Name { get; }
        int BaseLatencyMs { get; }
        double FailureProbability { get; }


        Response Process(Request request);
    }
}