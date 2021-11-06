using GS.Business.Infrastructure;
using GS.Business.Infrastructure.Command;
using GS.Business.Mapping;
using GS.Data.Repositories.UserWrite;
using GS.Domain.Models.User;
using System;
using System.Threading.Tasks;

namespace GS.Business.Command
{
    public class RegisterUserCommand : ICommand
    {
        public RegisterUserCommand(RegisterModel user)
        {
            User = user;
        }

        public RegisterModel User { get; set; }
    }

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IPasswordHashProvider _passwordHashProvider;

        public RegisterUserCommandHandler(IUserWriteRepository context, IPasswordHashProvider passwordHashProvider)
        {
            _userWriteRepository = context;
            _passwordHashProvider = passwordHashProvider;
        }

        public async Task Handle(RegisterUserCommand command)
        {
            var userEntity = command.User.ToEntity();

            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = _passwordHashProvider.GetPasswordHash(command.User.Password);

            await _userWriteRepository.AddUser(userEntity);
        }
    }
}
