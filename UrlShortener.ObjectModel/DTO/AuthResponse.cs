using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ObjectModel.DTO
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public AuthResponse()
        {
            
        }
        public AuthResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
