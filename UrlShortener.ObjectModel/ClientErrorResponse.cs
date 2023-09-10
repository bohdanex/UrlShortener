using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ObjectModel
{
    public class ClientErrorResponse
    {
        public string ErrorMessage { get; set; }
        public ClientErrorResponse()
        {
            
        }
        public ClientErrorResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
