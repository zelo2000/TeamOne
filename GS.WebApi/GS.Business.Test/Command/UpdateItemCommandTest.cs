using GS.Business.Command;
using GS.Data.Entities;
using GS.Data.Repositories.TripWrite;
using GS.Domain.Models.ItemToTake;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GS.Business.Test.Command
{
    public class UpdateItemCommandTest
    {
        private Mock<ITripWriteRepository> _tripWriteRepository;
        private UpdateItemCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _tripWriteRepository = new Mock<ITripWriteRepository>();
            _handler = new UpdateItemCommandHandler(_tripWriteRepository.Object);
        }

        [Test]
        public void UpdateItemCommand_ShouldUpdateItem()
        {
            var id = Guid.NewGuid();
            var model = new ItemToTakeBaseModel();

            _tripWriteRepository.Setup(x => x.UpdateItem(It.IsAny<Guid>(), It.IsAny<ItemToTake>()))
                .Returns(Task.CompletedTask);

            var command = new UpdateItemCommand(id, model);

            Assert.DoesNotThrowAsync(async () => await _handler.Handle(command));
        }
    }
}
