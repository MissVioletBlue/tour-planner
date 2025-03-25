namespace TourPlanner.Models.Models;

public class Tours
{
    public string Name {get; set;}
    public string Description {get; set;}
    public string StartLocation {get; set;}
    public string DestinationLocation {get; set;}
    public double Distance {get; set;}
    public TimeSpan EstimatedTime {get; set;}
    
}