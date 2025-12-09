using OopMonitoringLab.Metrics;

namespace OopMonitoringLab.Health
{
    public enum ServiceHealth { Healthy, Degraded, Unhealthy }

    public class ServiceHealthEvaluator
    {

        public double MaxHealthyErrorRate { get; set; } = 0.05;  
        public double MaxDegradedErrorRate { get; set; } = 0.20; 

        public int MaxHealthyLatencyMs { get; set; } = 150;      
        public int MaxDegradedLatencyMs { get; set; } = 400;     

        public ServiceHealth Evaluate(ServiceMetrics metrics)
        {
            if (metrics == null) return ServiceHealth.Unhealthy;

            if (metrics.TotalRequests == 0) return ServiceHealth.Healthy;

            bool errHealthy = metrics.ErrorRate <= MaxHealthyErrorRate;
            bool latHealthy = metrics.AverageLatencyMs <= MaxHealthyLatencyMs;
            if (errHealthy && latHealthy) return ServiceHealth.Healthy;

            bool errDegraded = metrics.ErrorRate <= MaxDegradedErrorRate;
            bool latDegraded = metrics.AverageLatencyMs <= MaxDegradedLatencyMs;
            if (errDegraded && latDegraded) return ServiceHealth.Degraded;

            return ServiceHealth.Unhealthy;
        }
    }
}
