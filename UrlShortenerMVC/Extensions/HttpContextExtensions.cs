﻿using Microsoft.AspNetCore.Http;
using System;

namespace UrlShortenerMVC.Extensions
{
    public static class HttpContextExtensions
    {
        public static string BaseUrl(this HttpRequest req)
        {
            if (req == null) return null;
            var uriBuilder = new UriBuilder(req.Scheme, req.Host.Host, req.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            return uriBuilder.Uri.AbsoluteUri;
        }
        public static string RedirectURL(this HttpRequest req, string redirectEndpointSymbol)
        {
            return BaseUrl(req) + $"{redirectEndpointSymbol}/";
        }
    }
}
