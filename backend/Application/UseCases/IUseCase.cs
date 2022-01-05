
namespace Application.UseCases {
    public interface ICommand {
        bool Success {get; set;}
        bool Done {get; set;}
    }
}