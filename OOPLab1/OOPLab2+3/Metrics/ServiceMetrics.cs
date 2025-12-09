using System;

namespace OopMonitoringLab.Metrics
{
    public class ServiceMetrics
    {
        public string ServiceName { get; }
        public int TotalRequests { get; private set; }
        public int SuccessfulRequests { get; private set; }
        public int FailedRequests { get; private set; }
        public double AverageLatencyMs { get; private set; }
        public int MaxLatencyMs { get; private set; }

        public double ErrorRate => TotalRequests == 0 ? 0 : (double)FailedRequests / TotalRequests;

        public ServiceMetrics(string serviceName)
        {
            ServiceName = serviceName ?? throw new ArgumentNullException(nameof(serviceName));
            TotalRequests = 0;
            SuccessfulRequests = 0;
            FailedRequests = 0;
            AverageLatencyMs = 0;
            MaxLatencyMs = 0;
        }

        public void Update(int latencyMs, bool success)
        {
            if (latencyMs < 0) latencyMs = 0;

            TotalRequests++;
            if (success) SuccessfulRequests++; else FailedRequests++;

            AverageLatencyMs = (AverageLatencyMs * (TotalRequests - 1) + latencyMs) / TotalRequests;

            if (latencyMs > MaxLatencyMs) MaxLatencyMs = latencyMs;
        }

        public ServiceMetricsSnapshot CreateSnapshot()
        {
            return new ServiceMetricsSnapshot
            {
                ServiceName = ServiceName,
                Timestamp = DateTime.UtcNow,
                TotalRequests = TotalRequests,
                SuccessfulRequests = SuccessfulRequests,
                FailedRequests = FailedRequests,
                AverageLatencyMs = AverageLatencyMs,
                MaxLatencyMs = MaxLatencyMs
            };
        }
    }
}
