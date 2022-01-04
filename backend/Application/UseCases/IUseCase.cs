
namespace Application.UseCases {

    public interface IUseCase<TInput, TOutput> {

        TOutput Execute(TInput input);

    }

    public interface ICommandHandler<ICommand> {

        ICommand Execute(ICommand command);
    }

    public interface ICommand {
        bool Success {get; set;}
        bool Done {get; set;}
    }
}