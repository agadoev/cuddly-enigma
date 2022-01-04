using System;

namespace Domain {

    public class Entity {
        public Guid Id {get; set;}

        public Entity() {
            this.Id = Guid.NewGuid();
        }
    }
}