using System.Collections.ObjectModel;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.DataAccess.Repositories;

public class TourRepository : ITourRepository
{
    private ObservableCollection<Tour> _tourList;

    public TourRepository()
    {
        _tourList = new ObservableCollection<Tour>();
    }
    
    public IEnumerable<Tour> GetAllTours() => _tourList;

    public Tour? GetTourById(Guid id) => _tourList.FirstOrDefault(t => t.Id == id);

    public void AddTour(Tour? tour)
    {
        ArgumentNullException.ThrowIfNull(tour);
        _tourList.Add(tour);
    }

    public void UpdateTour(Tour? tour)
    {
        ArgumentNullException.ThrowIfNull(tour);
        Tour? tempTour = _tourList.FirstOrDefault(x => x.Name == tour.Name);

        if (tempTour != null)
        {
            int index = _tourList.IndexOf(tempTour);
        
            if (index >= 0)
            {
                _tourList[index] = tour;
            }
        }
        else
        {
            throw new KeyNotFoundException($"Tour with name '{tour.Name}' not found.");
        }
    }

    public void DeleteTour(Guid id)
    {
        throw new NotImplementedException();
    }
}