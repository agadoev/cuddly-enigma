using Infrastructure.Entities;
using Domain;

namespace Infrastructure.EntityFrameworkDataAccess.Mappers {

    public class WishMapper : IMapper<Wish, WishEntity> {

        public Wish Map(WishEntity en) {
            var wish = new Wish();

            wish.Url = en.Url;
            wish.Id = en.Id;
            wish.Title = en.Title;
            wish.UserId = en.UserId;

            return wish;
        }

        public WishEntity Map(Wish w) {
            var entity = new WishEntity();

            entity.Id = w.Id;
            entity.Title = w.Title;
            entity.Url = w.Url;
            entity.UserId = w.UserId;

            return entity;
        }
    }
}