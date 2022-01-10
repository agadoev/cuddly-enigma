using System;
using Domain;

namespace Tests.Builders {
    public class ReservationBuilder {


        private Guid Id {get; set;}
        private User User {get; set;}
        private Wish Wish {get; set;}

        public ReservationBuilder WithId(Guid id) {
            this.Id = id;
            return this;
        }

        public ReservationBuilder WithUser (User user) {
            this.User = user;
            return this;
        }

        public ReservationBuilder WithWish (Wish wish) {
            this.Wish = wish;
            return this;
        }

        public Reservation Build() {
            var reservation = new Reservation();

            reservation.Id = this.Id;
            reservation.Wish = this.Wish;
            reservation.User = this.User;
            return reservation;
        }
    }
}