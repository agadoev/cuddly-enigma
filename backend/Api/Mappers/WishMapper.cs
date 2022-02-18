using Api.Dtos;
using Domain;
using Application.UseCases;

namespace Api.Mappers {

    public class WishMapper : IMapperBase<Wish, WishDto> {

        public Wish Map(WishDto dto) {
            var w = new Wish();

            w.Id = dto.WishId;
            w.Title = dto.Title;
            w.Url = dto.Url; 
            w.UserId = dto.UserId;

            return w;
        }

        public WishDto Map(Wish wish) {
            var dto = new WishDto();

            dto.WishId = wish.Id;
            dto.Url = wish.Url;
            dto.Title = wish.Title;
            dto.UserId = wish.UserId;

            return dto;
        }
    }
}