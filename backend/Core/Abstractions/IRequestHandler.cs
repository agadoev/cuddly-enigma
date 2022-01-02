

namespace Core.Abstractions{

    public interface IRequestHandler<T> where T : IRequest {

        void Handle(T Request);
    }
}