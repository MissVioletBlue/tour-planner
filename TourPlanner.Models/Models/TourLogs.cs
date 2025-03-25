using JetBrains.Annotations;

namespace TourPlanner.Models.Models;

public class TourLog
{
    [UsedImplicitly]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [UsedImplicitly]
    public Guid TourId { get; set; }
    
    [UsedImplicitly]
    public DateTime DateTime { get; set; } = DateTime.Now;
    
    [UsedImplicitly]
    public string Comment { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public int Difficulty { get; set; } = 3;
    
    [UsedImplicitly]
    public double TotalDistance { get; set; }
    
    [UsedImplicitly]
    public TimeSpan TotalTime { get; set; }
    
    [UsedImplicitly]
    public int Rating { get; set; } = 3;
}