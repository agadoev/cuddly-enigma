using Domain;
using System;

namespace Application.UseCases.CreateWish {
    public class CreateWishInput {
        public Guid UserId {get; set;}

        public string WishTitle {get; set;}

        public string WishUrl {get; set;}
    }
}