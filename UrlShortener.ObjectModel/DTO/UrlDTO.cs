using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ObjectModel.DTO
{
    public class UrlDTO
    {
        public Guid Id { get; set; }
        public string originalURL { get; set; }
        public string shortenedURL { get; set; }
        public DateTime creationDate { get; set; }
        public UserDTO User { get; set; }
    }
}
