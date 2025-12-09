using System;
using System.Collections.Generic;
using OopMonitoringLab.Models;
using OopMonitoringLab.Services;
using OopMonitoringLab.Metrics;
using OopMonitoringLab.Health;
using OopMonitoringLab.Observers;

namespace OopMonitoringLab
{
    public class Program
    {
        public static void MainTwo(string[] args)
        {
            try
            {
                var rnd = new Random();

                IService fast = new FastService("FastService", baseLatencyMs: 50, failureProbability: 0.05, rnd);
                IService slow = new SlowService("SlowService", baseLatencyMs: 200, failureProbability: 0.15, rnd);

                var metricsCollector = new InMemoryMetricsCollector();
                metricsCollector.RegisterService(fast);
                metricsCollector.RegisterService(slow);

                var consoleObserver = new ConsoleMetricObserver();
                var historyObserver = new HistoryMetricObserver();

                metricsCollector.MetricsUpdated += consoleObserver.HandleMetricsUpdated;
                metricsCollector.MetricsUpdated += historyObserver.HandleMetricsUpdated;

                var healthEvaluator = new ServiceHealthEvaluator();

                int totalRequests = 80;
                var serviceNames = new[] { fast.Name, slow.Name };
                var requests = new List<Request>(totalRequests);
                for (int i = 0; i < totalRequests; i++)
                {
                    string target = serviceNames[rnd.Next(serviceNames.Length)];
                    int payload = rnd.Next(50, 1001); 
                    int? deadline = (rnd.NextDouble() < 0.3) ? rnd.Next(100, 1001) : null; 
                    requests.Add(new Request(target, payload, deadline));
                }

                int printEvery = 10;
                for (int i = 0; i < requests.Count; i++)
                {
                    var req = requests[i];
                    IService svc = req.ServiceName == fast.Name ? fast : slow;

                    Response resp;
                    try
                    {
                        resp = svc.Process(req);
                    }
                    catch (Exception ex)
                    {
                        resp = new Response(false, 0, "EXC", ex.Message);
                    }

                    metricsCollector.Record(req, resp);

                    if ((i + 1) % printEvery == 0)
                    {
                        Console.WriteLine("\n=== Metrics snapshot after {0} requests ===", i + 1);
                        foreach (var m in metricsCollector.GetCurrentMetrics())
                        {
                            var health = healthEvaluator.Evaluate(m);
                            Console.WriteLine($"[Service: {m.ServiceName}] requests={m.TotalRequests}, errors={m.FailedRequests} (" +
                                              $"{m.ErrorRate:P1}), avgLatency={m.AverageLatencyMs:F1}ms, maxLatency={m.MaxLatencyMs}ms, health={health}");
                        }
                    }
                }

                Console.WriteLine("\n=== Final Summary ===");
                foreach (var m in metricsCollector.GetCurrentMetrics())
                {
                    var health = healthEvaluator.Evaluate(m);
                    Console.WriteLine($"[Service: {m.ServiceName}] requests={m.TotalRequests}, errors={m.FailedRequests} (" +
                                      $"{m.ErrorRate:P1}), avgLatency={m.AverageLatencyMs:F1}ms, maxLatency={m.MaxLatencyMs}ms, health={health}");
                }

                string chosen = fast.Name; 
                Console.WriteLine($"\n=== History fragment for service '{chosen}' (latest 5 entries) ===");
                var history = historyObserver.GetHistory(chosen);

                if (history == null || history.Count == 0)
                {
                    Console.WriteLine("(no history recorded)");
                }
                else
                {
                    int start = Math.Max(0, history.Count - 5);
                    for (int j = start; j < history.Count; j++)
                    {
                        var s = history[j];
                        Console.WriteLine($"[{s.Timestamp:HH:mm:ss}] reqs={s.TotalRequests}, errs={s.FailedRequests}, avg={s.AverageLatencyMs:F1}ms, max={s.MaxLatencyMs}ms");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal error: " + ex);
            }
        }
    }
}
