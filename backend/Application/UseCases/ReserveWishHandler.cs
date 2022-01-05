using System;
using Domain;
using Application.Repositories;

namespace Application.UseCases {

    public class ReserveWishCommand : ICommand {
        public bool Success {get; set;}
        public bool Done {get; set;}
        
        public Guid WishId {get; set;}
        public Guid ReserverId {get; set;}

        public ReserveWishCommand() {
            Success = false;
            Done = false;
        }
    }


    public class ReserveWishHandler : ICommandHandler<ReserveWishCommand>{

        // TODO: Вынести отдельный интерфейс IRepository<TModel>

        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWishesRepository _wishesRepository;

        public ReserveWishHandler(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IWishesRepository wishesRepository
        ) {
            _reservationRepository = reservationRepository;
            _userRepository = userRepository;
            _wishesRepository = wishesRepository;
        }

        public ReserveWishCommand Execute(ReserveWishCommand command) {

            var reserver = _userRepository.Get(command.ReserverId);

            var wish = _wishesRepository.Get(command.WishId);

            if (wish.Reserved)
                throw new ArgumentException("Wish is already reserved");

            wish.Reserved = true;

            var reservation = new Reservation(reserver, wish);

            _reservationRepository.Add(reservation);

            return new ReserveWishCommand();
        }
    }
}