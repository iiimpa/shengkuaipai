using System;

namespace WebApi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Proxy { get; set; }
        public string Ua { get; set; }
        public string Resolution { get; set; }
        public int Cost { get; set; }
        public int RealCost { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime ScheduleTime { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum TaskStatus
    {
        Wait,
        Running,
        NotFound,
        SurfaceError,
        Success,
        Failed
    }
}