using System;
using System.Net;

namespace Spotify.ObjectModel
{
    internal record Error : Object
    {
        internal Error(HttpStatusCode statusCode, String message) : base()
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        internal HttpStatusCode StatusCode { get; init; }
        internal String Message { get; init; }
    }
}