using System;

namespace UrlShortener.ObjectModel
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string SaltedHashedPassword { get; set; }
        public Role Role { get; set; }
    }
}
