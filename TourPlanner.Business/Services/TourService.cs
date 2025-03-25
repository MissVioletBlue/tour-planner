using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using TourPlanner.Models;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.Business.Services;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
    private readonly ObservableCollection<Tour> _tours = new();
    
    public ObservableCollection<Tour> Tours => _tours;
    
    public TourService(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
        RefreshTours();
    }
    
    private void RefreshTours()
    {
        _tours.Clear();
        foreach (var tour in _tourRepository.GetAllTours())
        {
            _tours.Add(tour);
        }
    }
    
    public Tour? GetTourById(Guid id) => _tourRepository.GetTourById(id);
    
    public bool AddTour(Tour? tour)
    {
        if (!ValidateTour(tour))
            return false;
            
        _tourRepository.AddTour(tour);
        RefreshTours();
        return true;
    }
    
    public bool UpdateTour(Tour? tour)
    {
        if (!ValidateTour(tour))
            return false;
            
        _tourRepository.UpdateTour(tour);
        RefreshTours();
        return true;
    }
    
    public bool DeleteTour(Guid id)
    {
        _tourRepository.DeleteTour(id);
        RefreshTours();
        return true;
    }
    
    private bool ValidateTour([NotNullWhen(true)] Tour? tour)
    {
        if (tour == null)
            return false;
        
        var requiredProperties = new[]
        {
            tour.Name,
            tour.Description,
            tour.StartLocation,
            tour.DestinationLocation,
            tour.TransportType,
            tour.RouteType,
            tour.SurfaceType,
            tour.DifficultyLevel,
        };

        return requiredProperties.All(prop => !string.IsNullOrWhiteSpace(prop));
    }
}