using Domain;
using System;

namespace Application.Repositories {
    public interface IWishesRepository {
        void Add(Wish wish);

        Wish Get(Guid id);
    }
}