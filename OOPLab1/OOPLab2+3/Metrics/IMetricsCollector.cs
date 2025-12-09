using System;
using System.Collections.Generic;
using OopMonitoringLab.Models;
using OopMonitoringLab.Observers;

namespace OopMonitoringLab.Metrics
{
    public interface IMetricsCollector
    {
        void RegisterService(Services.IService service);

        void Record(Request request, Models.Response response);

        IReadOnlyCollection<ServiceMetrics> GetCurrentMetrics();

        event EventHandler<ServiceMetricsUpdatedEventArgs>? MetricsUpdated;
    }
}
