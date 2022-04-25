using GS.Business.Command;
using GS.Data.Repositories.TripWrite;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class SetIsItemTakenCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private SetIsItemTakenCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new SetIsItemTakenCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void SetIsItemTakenCommand_ShouldSetIsItemTaken()
        {
            var id = Guid.NewGuid();

            _tripWriteRepository.Setup(x => x.SetIsItemTaken(It.IsAny<Guid>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            var command = new SetIsItemTakenCommand(id, true);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
