using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace TourPlanner.Models.Models;

public class Tour
{
    [UsedImplicitly]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [UsedImplicitly]
    public string Name { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public string Description { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public string StartLocation { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public string DestinationLocation { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public double Distance { get; set; }
    
    [UsedImplicitly]
    public TimeSpan EstimatedTime { get; set; }
    
    [UsedImplicitly]
    public string TransportType { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public int ChildFriendliness { get; set; } = 3;
    
    [UsedImplicitly]
    public int Popularity { get; set; } = 3;
    
    public byte[]? ImageData { get; set; }
    
    [UsedImplicitly]
    public string RouteType { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public string SurfaceType { get; set; } = string.Empty;
    
    [UsedImplicitly]
    public string DifficultyLevel { get; set; } = "Moderate";
    
    [UsedImplicitly]
    public ObservableCollection<TourLog> TourLogs { get; set; } = new();
}