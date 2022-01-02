
namespace Data.Entities {

    public class WishEntity : BaseEntity {
        public int Id {get; set;}

        public string Title {get; set;}

        public UserEntity User {get; set;}
        public int UserId {get; set;}
    }
}