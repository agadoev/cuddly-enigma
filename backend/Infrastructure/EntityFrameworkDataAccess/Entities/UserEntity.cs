using System;
using System.Collections.Generic;

namespace Infrastructure.Entities {

    public class UserEntity : Entity {
        public string Name {get; set;}

        public IEnumerable<WishEntity> Wishes {get; set;}
    }
}