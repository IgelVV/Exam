using System.Text.Json.Serialization;

namespace ExamVeshkin.Models
{
    public class TestRecord
    {
        private string _status;
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }
        [JsonPropertyName("status")]
        public string Status 
        { 
            get => _status;
            set { _status = value.ToUpper(); } 
        }

        public override bool Equals(object? obj)
        {
            return obj is TestRecord record &&
                   (Duration ?? string.Empty) == (record.Duration ?? string.Empty) &&
                   (Method ?? string.Empty) == (record.Method ?? string.Empty) &&
                   (Name ?? string.Empty) == (record.Name ?? string.Empty) &&
                   (StartTime ?? string.Empty) == (record.StartTime ?? string.Empty) &&
                   (EndTime ?? string.Empty) == (record.EndTime ?? string.Empty) &&
                   (Status ?? string.Empty) == (record.Status ?? string.Empty);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Duration, Method, Name, StartTime, EndTime, Status);
        }
    }
}
