using System.Collections.ObjectModel;
using TourPlanner.Models.Models;

namespace TourPlanner.Models.Interfaces;

public interface ITourLogService
{
    ObservableCollection<TourLog> GetTourLogsForTour(Guid tourId);
    TourLog? GetTourLogById(Guid id);
    bool AddTourLog(TourLog tourLog);
    bool UpdateTourLog(TourLog tourLog);
    bool DeleteTourLog(Guid id);
}