using AutoFixture;
using GS.Data.Entities;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using GS.Domain.Models.Configuration;
using GS.Domain.Models.Trip;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GS.Data.Test
{
    public class TripWriteRepositoryTest : BaseRepositoryTest
    {
        public Mock<IMongoClient> _client;
        public Mock<IMongoDatabase> _database;
        private Mock<IMongoCollection<Trip>> _tripCollection;
        private Mock<IAsyncCursor<Trip>> _tripCursor;
        private IEnumerable<Trip> _trips;

        private TripWriteRepository _repository;

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

            _repository = new TripWriteRepository(context);
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

            _tripCollection.Setup(x => x.UpdateOneAsync(It.IsAny<FilterDefinition<Trip>>(), It.IsAny<UpdateDefinition<Trip>>(), It.IsAny<UpdateOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Mock<UpdateResult>().Object);

            InitializeMongoDb();
        }

        [Test]
        public void UpdateTrip_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.UpdateTrip(Guid.NewGuid(), new TripBaseModel()));
        }

        [Test]
        public void SetTripStatus_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.SetTripStatus(Guid.NewGuid(), TripStatus.Closed));
        }

        [Test]
        public void AddToDoNode_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.AddToDoNode(Guid.NewGuid(), new ToDoNode()));
        }

        [Test]
        public void UpdateToDoNode_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.UpdateToDoNode(Guid.NewGuid(), new ToDoNode()));
        }

        [Test]
        public void SetToDoNodeStatus_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.SetToDoNodeStatus(Guid.NewGuid(), NodeStatus.Done));
        }

        [Test]
        public void DeleteToDoNode_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.DeleteToDoNode(Guid.NewGuid()));
        }

        [Test]
        public void AddItem_ShoulUpdateTripe()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.AddItem(Guid.NewGuid(), new ItemToTake()));
        }

        [Test]
        public void UpdateItem_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.UpdateItem(Guid.NewGuid(), new ItemToTake()));
        }

        [Test]
        public void SetIsItemTaken_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.SetIsItemTaken(Guid.NewGuid(), false));
        }

        [Test]
        public void DeleteItem_ShoulUpdateTrip()
        {
            Assert.DoesNotThrowAsync(async () => await _repository.DeleteItem(Guid.NewGuid()));
        }
    }
}
