using System;
using System.Collections.Generic;

namespace Infrastructure.Entities {
    public class EventEntity : Entity {

        public string Title {get; set;}
        public DateTime When { get; set; }
        public List<WishEntity> Wishes { get; set; }
    }
}