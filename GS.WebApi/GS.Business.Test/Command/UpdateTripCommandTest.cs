using GS.Business.Command;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.Trip;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class UpdateTripCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private UpdateTripCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new UpdateTripCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void UpdateItemCommand_ShouldUpdateItem()
        {
            var id = Guid.NewGuid();
            var model = new TripBaseModel();

            _tripWriteRepository.Setup(x => x.UpdateTrip(It.IsAny<Guid>(), It.IsAny<TripBaseModel>()))
                .Returns(Task.CompletedTask);

            var command = new UpdateTripCommand(id, model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
