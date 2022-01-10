using System;
using Domain;
using System.Collections.Generic;

namespace Tests.Builders {
    public class UserBuilder {
        
        private string Name {get; set;}
        private Guid Id {get; set;}

        private List<Wish> WishList {get; set;}


        public UserBuilder WithName(string name) {
            this.Name = name;

            return this;
        }

        public UserBuilder WithId(Guid id) {
            this.Id = id;
            return this;
        }

        public UserBuilder WithList(List<Wish> list) {
            this.WishList = list;
            return this;
        }


        public User Build() {
            var user = new User();

            user.Id = this.Id;
            user.Name = this.Name;
            user.Wishlist = this.WishList;
            return user;
        }
    }
}