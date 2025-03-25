using System.Collections.ObjectModel;
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
    
    public bool AddTour(Tour tour)
    {
        if (!ValidateTour(tour))
            return false;
            
        _tourRepository.AddTour(tour);
        RefreshTours();
        return true;
    }
    
    public bool UpdateTour(Tour tour)
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
    
    private bool ValidateTour(Tour tour)
    {
        // Validate required fields
        if (string.IsNullOrWhiteSpace(tour.Name))
            return false;
            
        if (string.IsNullOrWhiteSpace(tour.StartLocation))
            return false;
            
        if (string.IsNullOrWhiteSpace(tour.DestinationLocation))
            return false;
            
        // Add more validation as needed
        return true;
    }
}