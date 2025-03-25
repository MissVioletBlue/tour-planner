using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.DataAccess.Repositories;

public class TourLogRepository : ITourLogRepository
{
    private readonly List<TourLog> _tourLogs = new();

    public IEnumerable<TourLog> GetAllTourLogs() => _tourLogs.ToList();

    public IEnumerable<TourLog> GetTourLogsForTour(Guid tourId) => 
        _tourLogs.Where(tl => tl.TourId == tourId).ToList();

    public TourLog? GetTourLogById(Guid id) => 
        _tourLogs.FirstOrDefault(tl => tl.Id == id);

    public void AddTourLog(TourLog tourLog)
    {
        if (tourLog.Id == Guid.Empty)
            tourLog.Id = Guid.NewGuid();
            
        _tourLogs.Add(tourLog);
    }

    public void UpdateTourLog(TourLog tourLog)
    {
        var index = _tourLogs.FindIndex(tl => tl.Id == tourLog.Id);
        if (index >= 0)
        {
            _tourLogs[index] = tourLog;
        }
    }

    public void DeleteTourLog(Guid id)
    {
        var tourLog = GetTourLogById(id);
        if (tourLog != null)
        {
            _tourLogs.Remove(tourLog);
        }
    }
}