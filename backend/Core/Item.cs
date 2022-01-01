

namespace Core {
    public class Item : Entity {
        public int WishlistId {get; set;}

        public string Name {get; set;}

        public bool IsBooked {get; set;}
        
        public User BookedBy {get; set;}
    }
}