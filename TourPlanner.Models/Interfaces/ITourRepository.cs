using TourPlanner.Models.Models;

namespace TourPlanner.Models.Interfaces;

public interface ITourRepository
{
    IEnumerable<Tour> GetAllTours();
    Tour GetTourById(Guid id);
    void AddTour(Tour tour);
    void UpdateTour(Tour tour);
    void DeleteTour(Guid id);
}