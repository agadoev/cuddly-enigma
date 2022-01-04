using System;

namespace Domain {
    public class Reservation : Entity {

        public Guid Id {get; set;}

        public User User {get; set;}
        public Wish Wish {get; set;}


        public Reservation(User user, Wish wish) {
            Id = Guid.NewGuid();
            User = user;
            Wish = wish;
        }

    }
}