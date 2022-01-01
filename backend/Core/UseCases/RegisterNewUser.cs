

namespace Core.UseCases {

    public class RegisterNewUserCommand {
        public User user { get; set; }
    }


    public class RegisterNewUserHandler {

        private readonly IDbContext<User> _usersStorage;

        public RegisterNewUserHandler(
            IDbContext<User> usersStorage
        ) {
            _usersStorage = usersStorage;
        }

        public void Run(RegisterNewUserCommand command) {
            _usersStorage.Add(command.user);
        }
    }
}