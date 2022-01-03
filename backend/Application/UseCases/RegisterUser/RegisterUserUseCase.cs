using Application.Repositories;
using Domain;
using System;

namespace Application.UseCases.RegisterUser {

    public class RegisterUserUseCase : IUseCase<RegisterUserInput, RegisterUserOutput> {

        public readonly IUserRepository _repository;

        public RegisterUserUseCase(
            IUserRepository userRepository
        ) {
            _repository = userRepository;
        }

        public RegisterUserOutput Execute(RegisterUserInput input) {
            var output = new RegisterUserOutput();

            var user = new User() {
                Name = input.Name,
                Id = Guid.NewGuid()
            };

            _repository.Add(user);

            output.Success = true;

            return output;
        }
    }
}