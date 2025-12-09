using OopMonitoringLab.Models;


namespace OopMonitoringLab.Services
{
    public class FastService : ServiceBase
    {
        public FastService(string name, int baseLatencyMs, double failureProbability, System.Random rnd)
        : base(name, baseLatencyMs, failureProbability, rnd)
        {
        }


        public override Response Process(Request request)
        {
            var resp = Simulate(request);
            Log(request, resp);
            return resp;
        }
    }
}