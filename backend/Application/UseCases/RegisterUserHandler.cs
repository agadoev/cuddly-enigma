using Application.Repositories;
using Domain;
using System;

namespace Application.UseCases {

    public class RegisterUserCommand {
        public bool Success {get; set;}
        public bool Done {get; set;}

        public string Username {get; set;}

        public Guid? Id {get; set;}
    }


    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>  {

        public readonly IUserRepository _repository;

        public RegisterUserHandler(
            IUserRepository userRepository
        ) {
            _repository = userRepository;
        }

        public RegisterUserCommand Execute(RegisterUserCommand command) {
            
            // все проверки потом можно вынести в отдельный validation класс
            if (string.IsNullOrEmpty(command.Username))
                throw new ArgumentNullException();

            var user = new User() {
                Name = command.Username,
                Id = Guid.NewGuid()
            };

            _repository.Add(user);

            command.Id = user.Id;
            command.Success = true;
            command.Done = true;

            return command;
        }
    }
}