using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ObjectModel.DTO
{
    public class SimpleUrlDTO
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string ShortenedURL { get; set; }
        public string OriginalURL { get; set; }
    }
}
