namespace GS.Business.Infrastructure
{
    public interface IPasswordHashProvider
    {
        string GetPasswordHash(string password);
    }
}
