using System;

namespace Domain {
    public class Wish : Entity {
        public string Title {get; set;}

        public string Url {get; set;}

        public bool Reserved {get; set;}
        public Guid UserId {get; set;}
    }
}