using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TourPlanner.Business.Services;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.Business.Tests.Services;

[TestClass]
[TestCategory("TourLogService")]
public class TourLogServiceTest
{
    private ITourLogRepository _repository;
    private TourLogService _service;
    
    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<ITourLogRepository>();
        _service = new TourLogService(_repository);
    }
    
    [TestMethod]
    [Description("Adding an invalid tour log (e.g. with empty TourId) should return false.")]
    public void AddTourLog_InvalidTourLog_ReturnsFailure()
    {
        // Arrange
        var invalidLog = new TourLog
        {
            TourId = Guid.Empty,
            Comment = "Some comment",
            Difficulty = 3,
            Rating = 4,
            TotalDistance = 10,
            TotalTime = TimeSpan.FromMinutes(30)
        };
        
        // Act
        var result = _service.AddTourLog(invalidLog);
        
        // Assert
        Assert.IsFalse(result, "Expected AddTourLog to return false for an invalid tour log.");
        _repository.DidNotReceive().AddTourLog(Arg.Any<TourLog>());
    }
    
    [TestMethod]
    [Description("Adding a valid tour log should return true and call repository.AddTourLog.")]
    public void AddTourLog_ValidTourLog_ReturnsSuccess()
    {
        // Arrange
        var validLog = new TourLog
        {
            TourId = Guid.NewGuid(),
            Comment = "A good tour log",
            Difficulty = 3,
            Rating = 4,
            TotalDistance = 10,
            TotalTime = TimeSpan.FromMinutes(30)
        };
        
        // Act
        var result = _service.AddTourLog(validLog);
        
        // Assert
        Assert.IsTrue(result, "Expected AddTourLog to return true for a valid tour log.");
        _repository.Received(1).AddTourLog(validLog);
    }
    
    [TestMethod]
    [Description("Updating an invalid tour log should return false.")]
    public void UpdateTourLog_InvalidTourLog_ReturnsFailure()
    {
        // Arrange
        var invalidLog = new TourLog
        {
            TourId = Guid.NewGuid(),
            Comment = "Bad log",
            Difficulty = 3,
            Rating = 4,
            TotalDistance = -5,
            TotalTime = TimeSpan.FromMinutes(30)
        };
        
        // Act
        var result = _service.UpdateTourLog(invalidLog);
        
        // Assert
        Assert.IsFalse(result, "Expected UpdateTourLog to return false for an invalid tour log.");
        _repository.DidNotReceive().UpdateTourLog(Arg.Any<TourLog>());
    }
    
    [TestMethod]
    [Description("Updating a valid tour log should return true and call repository.UpdateTourLog.")]
    public void UpdateTourLog_ValidTourLog_ReturnsSuccess()
    {
        // Arrange
        var validLog = new TourLog
        {
            TourId = Guid.NewGuid(),
            Comment = "Updated log",
            Difficulty = 3,
            Rating = 4,
            TotalDistance = 15,
            TotalTime = TimeSpan.FromMinutes(45)
        };
        
        // Act
        var result = _service.UpdateTourLog(validLog);
        
        // Assert
        Assert.IsTrue(result, "Expected UpdateTourLog to return true for a valid tour log.");
        _repository.Received(1).UpdateTourLog(validLog);
    }
    
    [TestMethod]
    [Description("DeleteTourLog should call repository.DeleteTourLog and return true.")]
    public void DeleteTourLog_CallsRepositoryAndReturnsTrue()
    {
        // Arrange
        var logId = Guid.NewGuid();
        
        // Act
        var result = _service.DeleteTourLog(logId);
        
        // Assert
        Assert.IsTrue(result, "Expected DeleteTourLog to return true.");
        _repository.Received(1).DeleteTourLog(logId);
    }
    
    [TestMethod]
    [Description("GetTourLogById should return the correct tour log from the repository.")]
    public void GetTourLogById_ReturnsCorrectTourLog()
    {
        // Arrange
        var logId = Guid.NewGuid();
        var expectedLog = new TourLog
        {
            TourId = Guid.NewGuid(),
            Comment = "Test log",
            Difficulty = 3,
            Rating = 4,
            TotalDistance = 10,
            TotalTime = TimeSpan.FromMinutes(30)
        };
        _repository.GetTourLogById(logId).Returns(expectedLog);
        
        // Act
        var result = _service.GetTourLogById(logId);
        
        // Assert
        Assert.AreEqual(expectedLog, result, "Expected GetTourLogById to return the correct tour log.");
    }
    
    [TestMethod]
    [Description("GetTourLogsForTour should return an ObservableCollection containing the tour logs from the repository.")]
    public void GetTourLogsForTour_ReturnsCorrectCollection()
    {
        // Arrange
        var tourId = Guid.NewGuid();
        var logs = new List<TourLog>
        {
            new TourLog { TourId = tourId, Comment = "Log1", Difficulty = 2, Rating = 3, TotalDistance = 5, TotalTime = TimeSpan.FromMinutes(20) },
            new TourLog { TourId = tourId, Comment = "Log2", Difficulty = 4, Rating = 5, TotalDistance = 8, TotalTime = TimeSpan.FromMinutes(35) }
        };
        _repository.GetTourLogsForTour(tourId).Returns(logs);
        
        // Act
        var result = _service.GetTourLogsForTour(tourId);
        
        // Assert
        Assert.IsNotNull(result, "Expected GetTourLogsForTour to return a collection.");
        Assert.AreEqual(logs.Count, result.Count, "Expected the collection to contain the correct number of tour logs.");
        CollectionAssert.AreEqual(logs, result.ToList(), "Expected the tour logs in the collection to match those returned from the repository.");
    }
}