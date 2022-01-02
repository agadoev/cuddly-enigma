using System.Collections.Generic;

namespace Data.Entities {

    public class UserEntity : BaseEntity {
        public int Id {get; set;}

        public string Name {get; set;}

        public List<WishEntity> Wishes {get; set;} = new List<WishEntity>();
    }
}