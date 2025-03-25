namespace TourPlanner.Models.Models;

public class Tour
{
    public Guid Id { get; set; }
    public required string Name {get; set;}
    public required string Description {get; set;}
    public required string StartLocation {get; set;}
    public required string DestinationLocation {get; set;}
    public required string Distance {get; set;}
    public TimeSpan EstimatedTime {get; set;}
}