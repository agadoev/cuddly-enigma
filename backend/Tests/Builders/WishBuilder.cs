using System;
using Domain;

namespace Tests.Builders {
    public class WishBuilder {

        private Guid Id {get; set;}

        private bool Reserved {get; set;}

        private Guid UserId {get; set;}

        private string Title {get; set;}

        private string Url {get; set;}

        public WishBuilder WithId(Guid Id) {
            this.Id = Id;
            return this;
        }

        public WishBuilder WithTitle(string title) {
            this.Title = title;
            return this;
        }

        public WishBuilder WithReserved(bool reserved) {
            this.Reserved = Reserved;
            return this;
        }

        public WishBuilder WithUrl(string url) {
            this.Url = url;
            return this;
        }

        public WishBuilder WithUserId(Guid userId) {
            this.UserId = userId;
            return this;
        }


        public Wish Build() {

            var wish = new Wish();
            wish.Id = this.Id;
            wish.UserId = this.UserId;
            wish.Title = this.Title;
            wish.Url= this.Url;
            wish.Reserved = this.Reserved;

            return wish;
        }
    }
}