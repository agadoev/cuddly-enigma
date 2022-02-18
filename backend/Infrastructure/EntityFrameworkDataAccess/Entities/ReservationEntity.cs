using System;

namespace Infrastructure.Entities {

    public class ReservationEntity : Entity {

        public Guid ReserverId {get; set;}
        public UserEntity Reserver {get; set;}

        public Guid WishId {get; set;}
        public WishEntity Wish {get; set;}

        public DateTime Time {get; set;}

        public string Description { get; set; }
    }
}