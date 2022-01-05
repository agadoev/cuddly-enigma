
namespace Application.UseCases {

    public interface ICommandHandler<ICommand> {

        ICommand Execute(ICommand command);
    }
}