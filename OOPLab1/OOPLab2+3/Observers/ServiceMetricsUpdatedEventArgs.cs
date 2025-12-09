using System;
using OopMonitoringLab.Metrics;

namespace OopMonitoringLab.Observers
{
    public class ServiceMetricsUpdatedEventArgs : EventArgs
    {
        public ServiceMetricsSnapshot Snapshot { get; }

        public ServiceMetricsUpdatedEventArgs(ServiceMetricsSnapshot snapshot)
        {
            Snapshot = snapshot ?? throw new ArgumentNullException(nameof(snapshot));
        }
    }
}
