# Spotify.NET

A set of .NET libraries for interacting with the [Spotify Web API](https://spotify.dev/documentation/web-api).

## Project Structure

The Spotify.NET libraries are made up of multiple projects, allowing users to pick and choose exactly what they want to use. Below, you can find each one listed with a brief description of its dependencies and function.

### `Spotify.Abstractions`

Contains the core interfaces for the Spotify Web API. The Spotify.NET library provides a default implementation of these in the form of the `Spotify.Web.Requests` library, but feel free to implement your own!

### `Spotify.ObjectModel`

Contains types representing objects from the [Spotify Object Model](https://spotify.dev/documentation/web-api/reference/object-model).

### `Spotify.ObjectModel.Serialization`

> Depends on `Spotify.ObjectModel`

Contains types for serializing and deserializing types from the `Spotify.ObjectModel` namespace to and from JSON. All serialization-related functionality is built around the high-performance `System.Text.Json` library, which is included in the .NET standard library as of .NET Core 3.0.

### `Spotify.Web`

An internal library providing necessary types for basic HTTP interactions made with the Spotify Web API.

### `Spotify.Web.Authorization`

> Depends on `Spotify.Web`

Contains types used to authorize with the Spotify Web API and acquire access tokens. Originally designed to be used with `Spotify.Web.Requests`, the Spotify.NET implementation of the `Spotify.Abstractions` library, it is available as a separate package for users who want just a convenient way of authorizing with Spotify without the rest of the Spotify.NET features.

### `Spotify.Web.Navigation`

> Depends on `Spotify.Abstractions`
> Depends on `Spotify.ObjectModel`
> Depends on `Spotify.ObjectModel.Serialization`
> Depends on `Spotify.Web`
> Depends on `Spotify.Web.Authorization`

An optional extension library, `Spotify.Web.Navigation` defines extension methods for `Spotify.ObjectModel` types that allow quickly accessing additional sets of data from the Spotify Web API.

### `Spotify.Web.Requests`

> Depends on `Spotify.Abstractions`
> Depends on `Spotify.ObjectModel`
> Depends on `Spotify.ObjectModel.Serialization`
> Depends on `Spotify.Web`
> Depends on `Spotify.Web.Authorization`

The Spotify.NET default implementation of the `Spotify.Abstractions` library. This depends on all of the core libraries provided by Spotify.NET, and as such, can be thought of as the 'main' Spotify.NET package.