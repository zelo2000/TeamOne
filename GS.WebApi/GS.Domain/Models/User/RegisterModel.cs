namespace GS.Domain.Models.User
{
    public class RegisterModel : LogInModel
    {
        public string Username { get; set; }

        public string RepeatPassword { get; set; }
    }
}
