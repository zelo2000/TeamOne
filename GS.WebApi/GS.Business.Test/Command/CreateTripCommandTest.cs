using GS.Business.Command;
using GS.Business.Infrastructure;
using GS.Data.Entities;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using GS.Domain.Models.Trip;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class CreateTripCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private Mock<ITripStatusProvider> _tripStatusProvider;

        private CreateTripCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _tripStatusProvider = new Mock<ITripStatusProvider>();
            _handler = new CreateTripCommandHandler(_tripWriteRepository.Object, _tripStatusProvider.Object);
        }

        [Test]
        public void AddItemCommand_ShouldAddItem()
        {
            var id = Guid.NewGuid();
            var model = new TripBaseModel();

            _tripStatusProvider.Setup(x => x.GetStatus(It.IsAny<Trip>()))
                .Returns(TripStatus.InProgress);

            _tripWriteRepository.Setup(x => x.CreateTrip(It.IsAny<Trip>()))
                .Returns(Task.CompletedTask);

            var command = new CreateTripCommand(model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
