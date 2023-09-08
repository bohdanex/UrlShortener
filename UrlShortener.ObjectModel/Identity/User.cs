using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.ObjectModel
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string SaltedHashedPassword { get; set; }
        [Required]
        public Role Role { get; set; }
    }
}
