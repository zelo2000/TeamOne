using AutoFixture;

namespace GS.Data.Test
{
    public class BaseRepositoryTest
    {
        protected readonly Fixture _fixture;

        public BaseRepositoryTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
