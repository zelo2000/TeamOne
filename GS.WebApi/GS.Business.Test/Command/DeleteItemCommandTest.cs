using GS.Business.Command;
using GS.Data.Repositories.TripWrite;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class DeleteItemCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private DeleteItemCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new DeleteItemCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void DeleteItemItemCommand_ShouldDeleteItem()
        {
            var id = Guid.NewGuid();

            _tripWriteRepository.Setup(x => x.DeleteItem(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var command = new DeleteItemCommand(id);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
