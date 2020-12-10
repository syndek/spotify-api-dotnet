using System;

namespace Spotify.Web.RequestObjects
{
    internal readonly struct ReorderPlaylistItemsParameters
    {
        internal ReorderPlaylistItemsParameters(int rangeStart, int insertBefore, int? rangeLength, string? snapshotId)
        {
            this.RangeStart = rangeStart;
            this.InsertBefore = insertBefore;
            this.RangeLength = rangeLength;
            this.SnapshotId = snapshotId;
        }

        internal int RangeStart { get; }
        internal int InsertBefore { get; }
        internal int? RangeLength { get; }
        internal string? SnapshotId { get; }
    }
}