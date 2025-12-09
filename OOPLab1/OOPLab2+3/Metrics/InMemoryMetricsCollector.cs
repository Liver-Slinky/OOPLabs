using System;
using System.Collections.Generic;
using OopMonitoringLab.Models;
using OopMonitoringLab.Observers;

namespace OopMonitoringLab.Metrics
{
    public class InMemoryMetricsCollector : IMetricsCollector
    {
        private readonly Dictionary<string, ServiceMetrics> _metricsByService = new();
        private readonly object _sync = new();

        public event EventHandler<ServiceMetricsUpdatedEventArgs>? MetricsUpdated;

        public void RegisterService(Services.IService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            lock (_sync)
            {
                if (!_metricsByService.ContainsKey(service.Name))
                {
                    _metricsByService[service.Name] = new ServiceMetrics(service.Name);
                }
            }
        }

        public void Record(Request request, Response response)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (response == null) throw new ArgumentNullException(nameof(response));

            ServiceMetrics metrics;
            lock (_sync)
            {
                if (!_metricsByService.TryGetValue(request.ServiceName, out metrics))
                {

                    metrics = new ServiceMetrics(request.ServiceName);
                    _metricsByService[request.ServiceName] = metrics;
                }

                metrics.Update(response.LatencyMs, response.IsSuccess);

                var snapshot = metrics.CreateSnapshot();

                var capturedSnapshot = snapshot;
            }

            ServiceMetricsSnapshot snapshotToRaise;
            lock (_sync)
            {
                var current = _metricsByService[request.ServiceName];
                snapshotToRaise = current.CreateSnapshot();
            }

            try
            {
                MetricsUpdated?.Invoke(this, new ServiceMetricsUpdatedEventArgs(snapshotToRaise));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: observer threw during MetricsUpdated: {ex.Message}");
            }
        }

        public IReadOnlyCollection<ServiceMetrics> GetCurrentMetrics()
        {
            lock (_sync)
            {
                return new List<ServiceMetrics>(_metricsByService.Values).AsReadOnly();
            }
        }
    }
}
