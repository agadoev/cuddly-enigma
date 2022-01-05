using System;

namespace Api.Dtos {
    public class WishDto {
        public Guid WishId {get; set;}

        public Guid UserId {get; set;}

        public string Title {get; set;}
        
        public string Url {get; set;}
    }
}