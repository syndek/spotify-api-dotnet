using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents audio feature information for a <see cref="Track"/>.
    /// </summary>
    public record AudioFeatures : LocatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFeatures"/> record with the specified values.
        /// </summary>
        /// <param name="id">A <see cref="String"/> representing the Spotify ID of the <see cref="Track"/>.</param>
        /// <param name="uri">The Spotify URI of the <see cref="Track"/>.</param>
        /// <param name="trackHref">A link to the Spotify Web API endpoint providing full details of the <see cref="Track"/>.</param>
        /// <param name="analysisUrl">A link to the full audio analysis of the <see cref="Track"/>.</param>
        /// <param name="duration">An <see cref="Int32"/> representing the duration of the <see cref="Track"/> in milliseconds.</param>
        /// <param name="timeSignature">
        /// An <see cref="Int32"/> representing the estimated overall time signature of the <see cref="Track"/>.
        /// </param>
        /// <param name="key">An <see cref="Int32"/> representing the estimated overall key of the <see cref="Track"/>.</param>
        /// <param name="mode">A <see cref="Modality"/> value representing the modality of the <see cref="Track"/>.</param>
        /// <param name="acousticness">
        /// A <see cref="Single"/> representing a confidence measure of whether or not the <see cref="Track"/> is acoustic.
        /// </param>
        /// <param name="danceability">
        /// A <see cref="Single"/> representing a confidence measure of whether or not the <see cref="Track"/> is danceable.
        /// </param>
        /// <param name="energy">A <see cref="Single"/> representing how energetic the <see cref="Track"/> is.</param>
        /// <param name="instrumentalness">
        /// A <see cref="Single"/> representing a confidence measure of whether or not the <see cref="Track"/> is instrumental.
        /// </param>
        /// <param name="liveness">
        /// A <see cref="Single"/> representing a confidence measure of whether or not the <see cref="Track"/> was performed live.
        /// </param>
        /// <param name="loudness">A <see cref="Single"/> representing the loudness of the <see cref="Track"/> in decibels.</param>
        /// <param name="speechiness">A <see cref="Single"/> representing the presence of spoken words in the <see cref="Track"/>.</param>
        /// <param name="tempo">A <see cref="Single"/> representing the tempo of the <see cref="Track"/> in beats per minute.</param>
        /// <param name="valence">A <see cref="Single"/> representing the valence of the <see cref="Track"/>.</param>
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