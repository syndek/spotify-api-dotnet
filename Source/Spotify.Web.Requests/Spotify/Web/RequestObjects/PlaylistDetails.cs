namespace Spotify.Web.RequestObjects
{
    internal readonly struct PlaylistDetails
    {
        internal PlaylistDetails(string? name, string? description, bool? isPublic, bool? isCollaborative)
        {
            this.Name = name;
            this.Description = description;
            this.IsPublic = isPublic;
            this.IsCollaborative = isCollaborative;
        }

        internal string? Name { get; }
        internal string? Description { get; }
        internal bool? IsPublic { get; }
        internal bool? IsCollaborative { get; }
    }
}