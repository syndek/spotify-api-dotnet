using System;

namespace Spotify.ObjectModel
{
    internal record Error : Object
    {
        internal Error(Int32 statusCode, String message) : base()
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        internal Int32 StatusCode { get; init; }
        internal String Message { get; init; }
    }
}