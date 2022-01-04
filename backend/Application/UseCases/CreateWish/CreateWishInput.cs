using Domain;
using System;

namespace Application.UseCases.CreateWish {
    public class CreateWishInput {
        public Guid UserId {get; set;}

        public Wish Wish {get; set;}
    }
}