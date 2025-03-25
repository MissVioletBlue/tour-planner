namespace TourPlanner.Models.Models;

public class Tour
{
    public Guid Id { get; set; }
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required string StartLocation {get; set;}
    public required string DestinationLocation {get; set;}
    public required double Distance {get; set;}
    public TimeSpan EstimatedTime {get; set;}
    public string TransportType { get; set; } = string.Empty;
    public int ChildFriendliness { get; set; } = 3;
    public int Popularity { get; set; } = 3;
    public byte[]? ImageData { get; set; }
    public string RouteType { get; set; } = string.Empty;
    public string SurfaceType { get; set; } = string.Empty;
    public string DifficultyLevel { get; set; } = "Moderate";
}