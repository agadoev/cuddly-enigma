
namespace Application.UseCases {

    public interface IUseCase<TInput, TOutput> {

        TOutput Execute(TInput input);

    }
}