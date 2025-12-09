using System;
using OopMonitoringLab.Observers;

namespace OopMonitoringLab.Observers
{
    public class ConsoleMetricObserver
    {

        public void HandleMetricsUpdated(object? sender, ServiceMetricsUpdatedEventArgs e)
        {
            if (e?.Snapshot == null) return;
            var s = e.Snapshot;
            Console.WriteLine($"[Observer] {s.Timestamp:HH:mm:ss} {s.ServiceName}: reqs={s.TotalRequests}, errs={s.FailedRequests}, avg={s.AverageLatencyMs:F1}ms, max={s.MaxLatencyMs}ms");
        }
    }
}
