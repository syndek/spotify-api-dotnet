using System;

namespace Spotify.ObjectModel.EnumConverters
{
    internal static class ProductConverter : Object
    {
        internal static Product FromSpotifyString(String product) => product switch
        {
            "free" => Product.Free,
            "premium" => Product.Premium,
            _ => throw new ArgumentException($"Invalid {nameof(Product)} string value: {product}", nameof(product))
        };

        internal static String ToSpotifyString(this Product product) => product switch
        {
            Product.Free => "free",
            Product.Premium => "premium",
            _ => throw new ArgumentException($"Invalid {nameof(Product)} value: {product}", nameof(product))
        };
    }
}