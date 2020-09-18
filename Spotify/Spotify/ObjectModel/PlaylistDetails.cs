using System;

namespace Spotify.ObjectModel
{
    internal readonly struct PlaylistDetails
    {
        internal PlaylistDetails(String? name, String? description, Boolean? isPublic, Boolean? isCollaborative)
        {
            this.Name = name;
            this.Description = description;
            this.IsPublic = isPublic;
            this.IsCollaborative = isCollaborative;
        }

        internal String? Name { get; }
        internal String? Description { get; }
        internal Boolean? IsPublic { get; }
        internal Boolean? IsCollaborative { get; }
    }
}