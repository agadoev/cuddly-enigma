using System.Collections.Generic;

namespace Core {
    public class Wishlist : Entity {
        public int UserId {get; set;}
        public IEnumerable<Item> Items {get; set;}
    }
}