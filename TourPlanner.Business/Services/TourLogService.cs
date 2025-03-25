using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.Business.Services;

public class TourLogService : ITourLogService
{
    private readonly ITourLogRepository _tourLogRepository;

    public TourLogService(ITourLogRepository tourLogRepository)
    {
        _tourLogRepository = tourLogRepository;
    }

    public ObservableCollection<TourLog> GetTourLogsForTour(Guid tourId)
    {
        var tourLogs = new ObservableCollection<TourLog>(_tourLogRepository.GetTourLogsForTour(tourId));
        return tourLogs;
    }

    public TourLog? GetTourLogById(Guid id) => _tourLogRepository.GetTourLogById(id);

    public bool AddTourLog(TourLog tourLog)
    {
        if (!ValidateTourLog(tourLog))
            return false;
            
        _tourLogRepository.AddTourLog(tourLog);
        return true;
    }

    public bool UpdateTourLog(TourLog tourLog)
    {
        if (!ValidateTourLog(tourLog))
            return false;
            
        _tourLogRepository.UpdateTourLog(tourLog);
        return true;
    }

    public bool DeleteTourLog(Guid id)
    {
        _tourLogRepository.DeleteTourLog(id);
        return true;
    }
    
    private bool ValidateTourLog([NotNullWhen(true)] TourLog? tourLog)
    {
        if (tourLog == null)
            return false;
        
        var requiredStrings = new[] { tourLog.Comment };
        if (!requiredStrings.All(s => !string.IsNullOrWhiteSpace(s)))
            return false;
        
        return tourLog.TourId != Guid.Empty &&
               tourLog.Difficulty >= 1 && tourLog.Difficulty <= 5 &&
               tourLog.Rating >= 1 && tourLog.Rating <= 5 &&
               tourLog.TotalDistance >= 0 &&
               tourLog.TotalTime.TotalMinutes >= 0;
    }
}