using System;
namespace DO
{
    /// <summary>
    /// Represents an exit of a line
    /// </summary>
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        public TimeSpan StartAt { get; set; }
        public bool InService { get; set; }
    }
}
