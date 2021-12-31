using System.Collections.Generic;
using System.Collections;
using Core;

namespace Core.UseCases {

    public interface IDbContext<T> {

    }

    public class CreateWishlistCommand {
        User user;
        IEnumerable<Item> Items;
    }

    public class CreateWishlistHandler {


        public CreateWishlistHandler() {
            var wishlistContext = new 
        }

        public void Run(CreateWishlistCommand command) {

            
        }        
    }
}