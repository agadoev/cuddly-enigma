
namespace Infrastructure.EntityFrameworkDataAccess.Mappers {
    public interface IMapper<TFirst, TSecond> {
        TFirst Map(TSecond t);

        TSecond Map(TFirst t);
    }
}