using System.Collections.Generic;

namespace Core.UseCases {

    public interface IDbContext<T> where T : Entity {

        void LoadData();

        T[] GetAll();

        T? GetById(int id);

        void Add(IEnumerable<T> i);

        void Add(T item);

        void Commit();

        string GetPath();
    }
}