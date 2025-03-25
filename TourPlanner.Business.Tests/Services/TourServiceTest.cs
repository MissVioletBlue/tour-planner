using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TourPlanner.Business.Services;
using TourPlanner.Models.Interfaces;
using TourPlanner.Models.Models;

namespace TourPlanner.Business.Tests.Services;

[TestClass]
[TestCategory("TourService")]
public class TourServiceTest
{
    private TourService _service;
    private ITourRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = Substitute.For<ITourRepository>();
        _repository.GetAllTours().Returns(new List<Tour>());
        _service = new TourService(_repository);
    }

    [TestMethod]
    [Description("Adding a null tour should return a failure.")]
    public void AddTour_NullTour_ReturnsFailure()
    {
        // Act & Assert
        Assert.IsFalse(_service.AddTour(null), "Expected return value to be false for null tour.");
    }

    [TestMethod]
    [Description("Adding an incomplete tour should return a failure.")]
    public void AddTour_MissingRequiredProperty_ReturnsFailure()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Test",
            Description = "Test",
            StartLocation = "Test",
            DestinationLocation = "Test",
            TransportType = "Test",
            Distance = 5,
        };

        // Act & Assert
        Assert.IsFalse(_service.AddTour(tour), "Expected return value to be false for incomplete tour.");
    }

    [TestMethod]
    [Description("Adding a valid tour should return a success.")]
    public void AddTour_ValidTour_ReturnsSuccess()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Test",
            Description = "Test",
            StartLocation = "Test",
            DestinationLocation = "Test",
            TransportType = "Test",
            RouteType = "Test",
            SurfaceType = "Test",
            DifficultyLevel = "Test",
            Distance = 5,
        };
        
        _repository.GetAllTours().Returns(new List<Tour> { tour });

        // Act
        var result = _service.AddTour(tour);

        // Assert
        Assert.IsTrue(result, "Expected return value to be true for valid tour.");
        _repository.Received(1).AddTour(tour);
        Assert.IsTrue(_service.Tours.Contains(tour), "Expected tours collection to contain the added tour.");
    }

    [TestMethod]
    [Description("Updating a null tour should return a failure.")]
    public void UpdateTour_NullTour_ReturnsFailure()
    {
        // Act & Assert
        Assert.IsFalse(_service.UpdateTour(null), "Expected return value to be false for null tour.");
    }

    [TestMethod]
    [Description("Updating an incomplete tour should return a failure.")]
    public void UpdateTour_MissingRequiredProperty_ReturnsFailure()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Test",
            Description = "Test",
            StartLocation = "Test",
            DestinationLocation = "Test",
            TransportType = "Test",
            Distance = 5,
        };

        // Act & Assert
        Assert.IsFalse(_service.UpdateTour(tour), "Expected return value to be false for incomplete tour.");
    }

    [TestMethod]
    [Description("Updating a valid tour should return a success.")]
    public void UpdateTour_ValidTour_ReturnsSuccess()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Test",
            Description = "Test",
            StartLocation = "Test",
            DestinationLocation = "Test",
            TransportType = "Test",
            RouteType = "Test",
            SurfaceType = "Test",
            DifficultyLevel = "Test",
            Distance = 5,
        };
        
        _repository.GetAllTours().Returns(new List<Tour> { tour });

        // Act
        var result = _service.UpdateTour(tour);

        // Assert
        Assert.IsTrue(result, "Expected return value to be true for valid tour update.");
        _repository.Received(1).UpdateTour(tour);
        Assert.IsTrue(_service.Tours.Contains(tour), "Expected tours collection to contain the updated tour.");
    }

    [TestMethod]
    [Description("Deleting a tour should call repository delete and refresh the tours.")]
    public void DeleteTour_DeletesAndRefreshesTours()
    {
        // Arrange
        var tourId = Guid.NewGuid();
        _repository.GetAllTours().Returns(new List<Tour>());

        // Act
        var result = _service.DeleteTour(tourId);

        // Assert
        Assert.IsTrue(result, "Expected deletion to return true.");
        _repository.Received(1).DeleteTour(tourId);
        Assert.AreEqual(0, _service.Tours.Count, "Expected tours collection to be empty after deletion.");
    }

    [TestMethod]
    [Description("GetTourById should return the correct tour from repository.")]
    public void GetTourById_ReturnsCorrectTour()
    {
        // Arrange
        var tourId = Guid.NewGuid();
        var tour = new Tour
        {
            Name = "Test",
            Description = "Test",
            StartLocation = "Test",
            DestinationLocation = "Test",
            TransportType = "Test",
            RouteType = "Test",
            SurfaceType = "Test",
            DifficultyLevel = "Test",
            Distance = 5,
        };
        _repository.GetTourById(tourId).Returns(tour);

        // Act
        var result = _service.GetTourById(tourId);

        // Assert
        Assert.AreEqual(tour, result, "Expected GetTourById to return the correct tour.");
    }

    [TestMethod]
    [Description("Constructor should refresh tours from repository.")]
    public void Constructor_RefreshesTours()
    {
        // Arrange
        var tour1 = new Tour
        {
            Name = "Test1",
            Description = "Test1",
            StartLocation = "Test1",
            DestinationLocation = "Test1",
            TransportType = "Test1",
            RouteType = "Test1",
            SurfaceType = "Test1",
            DifficultyLevel = "Test1",
            Distance = 5,
        };
        var tour2 = new Tour
        {
            Name = "Test2",
            Description = "Test2",
            StartLocation = "Test2",
            DestinationLocation = "Test2",
            TransportType = "Test2",
            RouteType = "Test2",
            SurfaceType = "Test2",
            DifficultyLevel = "Test2",
            Distance = 10,
        };
        _repository.GetAllTours().Returns(new List<Tour> { tour1, tour2 });

        // Act
        _service = new TourService(_repository);

        // Assert
        Assert.AreEqual(2, _service.Tours.Count, "Expected two tours to be refreshed from repository.");
    }
}