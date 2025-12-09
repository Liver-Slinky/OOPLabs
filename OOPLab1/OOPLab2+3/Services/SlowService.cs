using OopMonitoringLab.Models;


namespace OopMonitoringLab.Services
{
    public class SlowService : ServiceBase
    {
        public SlowService(string name, int baseLatencyMs, double failureProbability, System.Random rnd)
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