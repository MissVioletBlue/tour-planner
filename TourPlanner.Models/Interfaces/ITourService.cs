using TourPlanner.Models.Models;

namespace TourPlanner.Models.Interfaces;

public interface ITourService
{
    IEnumerable<Tour> GetAllTours();
    Tour? GetTourById(Guid id);
    bool AddTour(Tour tour);
    bool UpdateTour(Tour tour);
    bool DeleteTour(Guid id);
}