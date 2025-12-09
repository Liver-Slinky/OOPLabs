using System;
using OopMonitoringLab.Models;


namespace OopMonitoringLab.Services
{
    public abstract class ServiceBase : IService
    {
        private readonly Random _rnd;


        public string Name { get; }
        public int BaseLatencyMs { get; }
        public double FailureProbability { get; }


        protected ServiceBase(string name, int baseLatencyMs, double failureProbability, Random rnd)
        {
            Name = name;
            BaseLatencyMs = baseLatencyMs;
            FailureProbability = failureProbability;
            _rnd = rnd ?? new Random();
        }


        public abstract Response Process(Models.Request request);


        protected Response Simulate(Request request)
        {
            int jitter = _rnd.Next(-BaseLatencyMs / 2, BaseLatencyMs / 2 + 1);
            int latency = Math.Max(0, BaseLatencyMs + jitter);

            bool failed = _rnd.NextDouble() < FailureProbability;


            if (request.DeadlineMs.HasValue && latency > request.DeadlineMs.Value)
            {
                return new Response(false, latency, "TIMEOUT", "Deadline exceeded");
            }


            if (failed)
            {
                return new Response(false, latency, "ERR_FAIL", "Simulated failure");
            }


            return new Response(true, latency);
        }


        protected virtual void Log(Request request, Response response)
        {
            Console.WriteLine($"[Service:{Name}] processed request payload={request.PayloadSize}, latency={response.LatencyMs}ms, success={response.IsSuccess}");
        }
    }
}