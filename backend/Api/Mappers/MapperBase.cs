
namespace Api.Mappers {

    public interface IMapperBase<TFirst, TSecond> {

        TFirst Map(TSecond m);
        TSecond Map(TFirst m);

    }
}