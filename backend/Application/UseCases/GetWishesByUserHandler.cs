using System;
using Domain;
using System.Collections.Generic;
using Application.Repositories;

namespace Application.UseCases {

    public class GetWishesByUserCommand : ICommand {

        public bool Success {get; set;}
        public bool Done {get; set;}

        public Guid? UserId {get; set;}

        public IEnumerable<Wish> Wishes {get; set;}
    }

    public class GetWishesByUserHandler : ICommandHandler<GetWishesByUserCommand> {

        private IWishesRepository _wishesRepository;

        public GetWishesByUserHandler(IWishesRepository wishesRepository) {
            _wishesRepository = wishesRepository;
        }

        public void Execute(GetWishesByUserCommand command) {

            if (command.UserId is null)
                throw new ArgumentNullException();

            command.Wishes = _wishesRepository.GetByUser(command.UserId.Value);
            command.Success = true;
            command.Done = true;

        }
    }
}