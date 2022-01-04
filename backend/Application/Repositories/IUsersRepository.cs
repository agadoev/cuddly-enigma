using System;

namespace Application.Repositories {
    using Domain;

    public interface IUserRepository {
        void Add(User user);

        User Get(Guid id);
    }
}