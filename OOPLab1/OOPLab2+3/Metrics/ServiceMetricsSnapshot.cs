using System;

namespace OopMonitoringLab.Metrics
{
    public class ServiceMetricsSnapshot
    {
        public string ServiceName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public int TotalRequests { get; set; }
        public int SuccessfulRequests { get; set; }
        public int FailedRequests { get; set; }
        public double AverageLatencyMs { get; set; }
        public int MaxLatencyMs { get; set; }
    }
}
