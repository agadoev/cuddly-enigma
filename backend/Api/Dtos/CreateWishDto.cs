using System;

namespace Api.Dtos {
    public class CreateWishDto {

        public Guid UserId {get; set;}

        public string Title {get; set;}
        public string Url {get ;set;}

        public Guid CreatedWishId { get; set;}
    }
}