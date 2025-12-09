namespace OopMonitoringLab.Models
{
    public class Response
    {
        public bool IsSuccess { get; }
        public int LatencyMs { get; }
        public string? ErrorCode { get; }
        public string? ErrorMessage { get; }


        public Response(bool isSuccess, int latencyMs, string? errorCode = null, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            LatencyMs = latencyMs;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}