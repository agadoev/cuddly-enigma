
namespace Domain {
    public class Reservation : Entity {

        public User User {get; set;}
        public Wish Wish {get; set;}


        public Reservation(User user, Wish wish) {
            User = user;
            Wish = wish;
        }

    }
}