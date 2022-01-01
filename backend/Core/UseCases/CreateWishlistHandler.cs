using System.Collections.Generic;
using System.Collections;
using Core;

namespace Core.UseCases {


    public class CreateWishlistCommand {
        public User user;
        public IEnumerable<Item> Items;
    }

    public class CreateWishlistHandler {

        private readonly IDbContext<Wishlist> _wishlishContext;

        public CreateWishlistHandler(IDbContext<Wishlist> wishlistContext) {
            _wishlishContext = wishlistContext;
        }

        public void Run(CreateWishlistCommand command) {

            var wishlist = new Wishlist() {
                Id = 12,
                UserId = command.user.Id,
                Items = command.Items
            };

            _wishlishContext.Add(wishlist);
        }        
    }
}