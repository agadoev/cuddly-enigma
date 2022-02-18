using System;

namespace Infrastructure.Entities {

    public class AccountEntity : Entity {

        public string MailAddress { get; set; }

        public string PasswordHash { get;set; }

        public DateTime LastSeen {get; set;}

        public UserEntity User {get; set;}

        public Guid UserId {get; set;}

    }
}