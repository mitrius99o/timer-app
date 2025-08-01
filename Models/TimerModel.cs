namespace TimerApp.Models
{
    public class TimerModel
    {
        public int Id { get; set; }
        public required string Name { get; set; } = "Таймер";
        public TimeSpan Duration { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public bool IsRunning { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class TimerRequest
    {
        public required string Name { get; set; } = "Таймер";
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }

    public class TimerResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCompleted { get; set; }
        public required string Status { get; set; }
    }
} 