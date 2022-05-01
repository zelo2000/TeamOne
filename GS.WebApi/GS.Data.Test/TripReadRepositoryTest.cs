using AutoFixture;
using FluentAssertions;
using GS.Data.Entities;
using GS.Data.Repositories.TripRead;
using GS.Domain.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Data.Test
{
    public class TripReadRepositoryTest : BaseRepositoryTest
    {
        public Mock<IMongoClient> _client;
        public Mock<IMongoDatabase> _database;
        private Mock<IMongoCollection<Trip>> _tripCollection;
        private Mock<IAsyncCursor<Trip>> _tripCursor;
        private IEnumerable<Trip> _trips;

        private TripReadRepository _repositpry;

        [SetUp]
        public void SetUp()
        {
            _trips = _fixture.CreateMany<Trip>(2);

            _database = new Mock<IMongoDatabase>();
            _client = new Mock<IMongoClient>();
            _tripCollection = new Mock<IMongoCollection<Trip>>();
            _tripCursor = new Mock<IAsyncCursor<Trip>>();

            InitializeMongoProductCollection();

            var mongoDbSettings = Options.Create(new MongoDbSettings() { DatabaseName = "test", ConnectionString = "test" });
            var context = new TripDbContext(_client.Object, mongoDbSettings);

            _repositpry = new TripReadRepository(context);
        }

        private void InitializeMongoDb()
        {
            _database.Setup(x => x.GetCollection<Trip>(It.IsAny<string>(), default))
                     .Returns(_tripCollection.Object);

            _client.Setup(x => x.GetDatabase(It.IsAny<string>(), default))
                .Returns(_database.Object);
        }

        private void InitializeMongoProductCollection()
        {
            _tripCursor.Setup(x => x.Current).Returns(_trips);

            _tripCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _tripCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            _tripCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Trip>>(), It.IsAny<FindOptions<Trip, Trip>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_tripCursor.Object);

            InitializeMongoDb();
        }

        [Test]
        public async Task GetUserTrips_ShouldReturnTrips()
        {
            var result = await _repositpry.GetUserTrips(Guid.Empty);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Test]
        public async Task GetTripById_ShouldReturnTrip()
        {
            var result = await _repositpry.GetTripById(Guid.Empty);

            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetItemsToTake_ShouldReturnItemsToTake()
        {
            var itemsToTake = _trips.FirstOrDefault().ItemsToTake;

            var result = await _repositpry.GetItemsToTake(Guid.Empty);

            result.Should().NotBeNull();
            result.Should().HaveCount(itemsToTake.Count);
        }

        [Test]
        public async Task GetToDoNodes_ShouldReturnToDoNodes()
        {
            var toDoNodes = _trips.FirstOrDefault().ToDoNodes;

            var result = await _repositpry.GetToDoNodes(Guid.Empty);

            result.Should().NotBeNull();
            result.Should().HaveCount(toDoNodes.Count);
        }
    }
}
