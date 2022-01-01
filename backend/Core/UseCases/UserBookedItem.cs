using Core;

namespace Core.UseCases {

    public class UserBookedItemCommand {
        public User BookedBy { get; set; }

        public int ChangedItemId {get; set;}
    }

    public class UserBookedItemHandler {

        private readonly IDbContext<Wishlist> _wishlistStorage;
        private readonly IDbContext<Item> _itemsStorage;

        public UserBookedItemHandler(
            IDbContext<Wishlist> wishlistStorage,
            IDbContext<Item> itemsStorage
        ) {
            _wishlistStorage = wishlistStorage;
            _itemsStorage = itemsStorage;
        }

        public void Run(UserBookedItemCommand command) {
            /*
                1. Найти вишлист, который изменили
                2. Найти итем, который изменили
                3. Поменять итем, который изменили
                4. Сохранить изменения
                5. Оповестить клиента об изменении
            */

            var changedItem = _itemsStorage.GetById(command.ChangedItemId);

            if (changedItem is null)
                return;

            changedItem.IsBooked = true;
            changedItem.BookedBy = command.BookedBy;

            _itemsStorage.Commit();
        }
    }
}