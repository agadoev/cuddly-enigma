using System;

namespace Api.Dtos {
    public class CreateWishDto {

        public Guid UserId {get; set;}

        public WishDto WishDto {get; set;}
    }
}