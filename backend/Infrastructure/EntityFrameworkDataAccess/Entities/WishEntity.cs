using System;

namespace Infrastructure.Entities {
    public class WishEntity : Entity {

        public string Title {get; set;}

        public string Url {get; set;}

        public Guid UserId {get; set;}
        public UserEntity User {get; set;}
    }
}