using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.Business.Services;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
        
    public TourService(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }
        
    public IEnumerable<Tour> GetAllTours() => _tourRepository.GetAllTours();
        
    public Tour? GetTourById(Guid id) => _tourRepository.GetTourById(id);
        
    public bool AddTour(Tour tour)
    {
        if (!ValidateTour(tour))
            return false;
                
        _tourRepository.AddTour(tour);
        return true;
    }
        
    public bool UpdateTour(Tour tour)
    {
        if (!ValidateTour(tour))
            return false;
                
        _tourRepository.UpdateTour(tour);
        return true;
    }
        
    public bool DeleteTour(Guid id)
    {
        _tourRepository.DeleteTour(id);
        return true;
    }
        
    private bool ValidateTour(Tour tour)
    {
        if (string.IsNullOrWhiteSpace(tour.Name))
            return false;
                
        if (string.IsNullOrWhiteSpace(tour.StartLocation))
            return false;
                
        if (string.IsNullOrWhiteSpace(tour.DestinationLocation))
            return false;
                
        // TODO: More Validations
        return true;
    }
}