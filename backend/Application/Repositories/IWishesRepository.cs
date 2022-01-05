using Domain;
using System.Collections.Generic;
using System;

namespace Application.Repositories {
    public interface IWishesRepository {
        void Add(Wish wish);

        Wish Get(Guid id);

        IEnumerable<Wish> GetByUser(Guid userId);

        void Remove(Guid id);
    }
}