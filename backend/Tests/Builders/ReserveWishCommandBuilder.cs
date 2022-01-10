using Domain;
using Application.UseCases;
using System;


namespace Tests.Builders {
    public class ReserveWishCommandBuilder {


        private bool Success {get; set;}
        private bool Done {get; set;}
        
        private Guid WishId {get; set;}
        private Guid ReserverId {get; set;}

        public ReserveWishCommandBuilder WithWishId(Guid wishId) {
            this.WishId = wishId;
            return this;
        }

        public ReserveWishCommandBuilder WithReserverId(Guid reserverId) {
            this.ReserverId = reserverId;
            return this;
        }

        public ReserveWishCommandBuilder WithSuccess(bool success) {
            this.Success = success;
            return this;
        }

        public ReserveWishCommandBuilder WithDone(bool done) {
            this.Done = done;
            return this;
        }

        public ReserveWishCommand Build() {
            var command = new ReserveWishCommand();
            command.Done = this.Done;
            command.Success = this.Success;
            command.ReserverId = this.ReserverId;
            command.WishId = this.WishId;
            return command;
        }

    }
}