using TourPlanner.Models.Models;

namespace TourPlanner.Models.Interfaces;

public interface ITourLogRepository
{
    IEnumerable<TourLog> GetAllTourLogs();
    IEnumerable<TourLog> GetTourLogsForTour(Guid tourId);
    TourLog? GetTourLogById(Guid id);
    void AddTourLog(TourLog tourLog);
    void UpdateTourLog(TourLog tourLog);
    void DeleteTourLog(Guid id);
}