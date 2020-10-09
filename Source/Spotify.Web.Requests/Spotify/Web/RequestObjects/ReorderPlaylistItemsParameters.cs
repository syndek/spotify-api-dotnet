using System;

namespace Spotify.Web.RequestObjects
{
    internal readonly struct ReorderPlaylistItemsParameters
    {
        internal ReorderPlaylistItemsParameters(Int32 rangeStart, Int32 insertBefore, Int32? rangeLength, String? snapshotId)
        {
            this.RangeStart = rangeStart;
            this.InsertBefore = insertBefore;
            this.RangeLength = rangeLength;
            this.SnapshotId = snapshotId;
        }

        internal Int32 RangeStart { get; }
        internal Int32 InsertBefore { get; }
        internal Int32? RangeLength { get; }
        internal String? SnapshotId { get; }
    }
}