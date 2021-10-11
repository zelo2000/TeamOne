namespace GS.Business.Infrastructure
{
    public interface ICommandHandler<T>
    {
        public void Handle(T command);
    }
}
