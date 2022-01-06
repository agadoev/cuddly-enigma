
namespace Application.UseCases {

    public interface ICommandHandler<ICommand> {
        void Execute(ICommand command);
    }
}