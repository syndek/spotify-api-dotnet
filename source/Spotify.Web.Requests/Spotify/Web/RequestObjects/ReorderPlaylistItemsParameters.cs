namespace Spotify.Web.RequestObjects
{
    internal readonly struct ReorderPlaylistItemsParameters
    {
        internal ReorderPlaylistItemsParameters(int rangeStart, int insertBefore, int? rangeLength, string? snapshotId)
        {
            RangeStart = rangeStart;
            InsertBefore = insertBefore;
            RangeLength = rangeLength;
            SnapshotId = snapshotId;
        }

        internal int RangeStart { get; }
        internal int InsertBefore { get; }
        internal int? RangeLength { get; }
        internal string? SnapshotId { get; }
    }
}
