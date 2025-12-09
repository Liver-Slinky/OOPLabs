using System.Collections.Generic;
using OopMonitoringLab.Metrics;

namespace OopMonitoringLab.Observers
{
    public class HistoryMetricObserver
    {
        private readonly Dictionary<string, List<ServiceMetricsSnapshot>> _history = new();

        public void HandleMetricsUpdated(object? sender, ServiceMetricsUpdatedEventArgs e)
        {
            if (e?.Snapshot == null) return;
            var s = e.Snapshot;

            if (!_history.TryGetValue(s.ServiceName, out var list))
            {
                list = new List<ServiceMetricsSnapshot>();
                _history[s.ServiceName] = list;
            }

            list.Add(s);
        }

        public IReadOnlyList<ServiceMetricsSnapshot> GetHistory(string serviceName)
        {
            if (serviceName == null) return new List<ServiceMetricsSnapshot>().AsReadOnly();
            if (_history.TryGetValue(serviceName, out var list)) return list.AsReadOnly();
            return new List<ServiceMetricsSnapshot>().AsReadOnly();
        }
    }
}
