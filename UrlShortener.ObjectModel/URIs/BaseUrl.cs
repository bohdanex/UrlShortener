using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ObjectModel.UriModels
{
    public class BaseUrl
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
        public string ShortenedURL { get; set; }
        [Required]
        public string OriginalURL { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public override string ToString()
        {
            return OriginalURL;
        }
    }
}
