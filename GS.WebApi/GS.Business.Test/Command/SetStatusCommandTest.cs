using GS.Business.Command;
using GS.Business.Infrastructure;
using GS.Data.Entities;
using GS.Data.Repositories.TripRead;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class SetStatusCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private Mock<ITripReadRepository> _tripReadRepository;
        private Mock<ITripStatusProvider> _tripStatusProvider;

        private SetStatusCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _tripReadRepository = new Mock<ITripReadRepository>();
            _tripStatusProvider = new Mock<ITripStatusProvider>();

            _handler = new SetStatusCommandHandler(_tripWriteRepository.Object, _tripReadRepository.Object, _tripStatusProvider.Object);
        }

        [Test]
        public void SetStatusCommand_ShouldAddItem()
        {
            var id = Guid.NewGuid();

            _tripReadRepository.Setup(x => x.GetTripById(It.IsAny<Guid>()))
                .ReturnsAsync(new Trip());

            _tripStatusProvider.Setup(x => x.GetStatus(It.IsAny<Trip>()))
                .Returns(TripStatus.Planned);

            _tripWriteRepository.Setup(x => x.SetIsItemTaken(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            var command = new SetStatusCommand(id);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
