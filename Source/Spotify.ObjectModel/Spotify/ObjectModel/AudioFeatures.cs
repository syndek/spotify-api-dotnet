using System;

namespace Spotify.ObjectModel
{
    public record AudioFeatures : LocatableObject
    {
        public AudioFeatures(
            String id,
            Uri uri,
            Uri trackHref,
            Uri analysisUrl,
            Int32 duration,
            Int32 timeSignature,
            Int32 key,
            Int32 mode,
            Single acousticness,
            Single danceability,
            Single energy,
            Single instrumentalness,
            Single liveness,
            Single loudness,
            Single speechiness,
            Single tempo,
            Single valence) :
            base(id, uri)
        {
            this.TrackHref = trackHref;
            this.AnalysisUrl = analysisUrl;
            this.Duration = duration;
            this.TimeSignature = timeSignature;
            this.Key = key;
            this.Mode = mode;
            this.Acousticness = acousticness;
            this.Danceability = danceability;
            this.Energy = energy;
            this.Instrumentalness = instrumentalness;
            this.Liveness = liveness;
            this.Loudness = loudness;
            this.Speechiness = speechiness;
            this.Tempo = tempo;
            this.Valence = valence;
        }

        public Uri TrackHref { get; init; }
        public Uri AnalysisUrl { get; init; }
        public Int32 Duration { get; init; }
        public Int32 TimeSignature { get; init; }
        public Int32 Key { get; init; }
        public Int32 Mode { get; init; }
        public Single Acousticness { get; init; }
        public Single Danceability { get; init; }
        public Single Energy { get; init; }
        public Single Instrumentalness { get; init; }
        public Single Liveness { get; init; }
        public Single Loudness { get; init; }
        public Single Speechiness { get; init; }
        public Single Tempo { get; init; }
        public Single Valence { get; init; }
    }
}