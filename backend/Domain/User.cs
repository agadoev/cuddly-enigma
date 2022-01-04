using System;
using System.Collections.Generic;

namespace Domain {
    public class User : Entity {

        public string Name {get; set;}        
        public List<Wish> Wishlist {get; set;}

        public User() {
            Id = Guid.NewGuid();
            Wishlist = new List<Wish>();
        }

        public void AddWish(Wish wish) {
            this.Wishlist.Add(wish);
        }

    }
}