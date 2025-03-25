using System.Collections.ObjectModel;
using TourPlanner.Models.Models;

namespace TourPlanner.Models.Interfaces;

public interface ITourService
{
    ObservableCollection<Tour> Tours { get; }
    Tour? GetTourById(Guid id);
    bool AddTour(Tour tour);
    bool UpdateTour(Tour tour);
    bool DeleteTour(Guid id);
}