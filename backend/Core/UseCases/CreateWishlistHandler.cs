using System.Collections.Generic;
using System.Collections;
using Core;
using Core.Abstractions;

namespace Core.UseCases {

    public class CreateWishlistCommand : IRequest {
        public User user;
        public IEnumerable<Item> Items;
    }

    public class CreateWishlistHandler : IRequestHandler<CreateWishlistCommand> {

        private readonly IDbContext<Wishlist> _wishlishContext;

        public CreateWishlistHandler(IDbContext<Wishlist> wishlistContext) {
            _wishlishContext = wishlistContext;
        }

        public void Handle(CreateWishlistCommand command) {

            var wishlist = new Wishlist() {
                Id = 12,
                UserId = command.user.Id,
                Items = command.Items
            };

            _wishlishContext.Add(wishlist);
        }        
    }
}