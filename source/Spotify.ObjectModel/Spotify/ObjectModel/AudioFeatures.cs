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
        /// <param name="id">A <see cref="string"/> representing the Spotify ID of the <see cref="Track"/>.</param>
        /// <param name="uri">The Spotify URI of the <see cref="Track"/>.</param>
        /// <param name="trackHref">A link to the Spotify Web API endpoint providing full details of the <see cref="Track"/>.</param>
        /// <param name="analysisUrl">A link to the full audio analysis of the <see cref="Track"/>.</param>
        /// <param name="duration">An <see cref="int"/> representing the duration of the <see cref="Track"/> in milliseconds.</param>
        /// <param name="timeSignature">
        /// An <see cref="int"/> representing the estimated overall time signature of the <see cref="Track"/>.
        /// </param>
        /// <param name="key">An <see cref="int"/> representing the estimated overall key of the <see cref="Track"/>.</param>
        /// <param name="mode">A <see cref="Modality"/> value representing the modality of the <see cref="Track"/>.</param>
        /// <param name="acousticness">
        /// A <see cref="float"/> representing a confidence measure of whether or not the <see cref="Track"/> is acoustic.
        /// </param>
        /// <param name="danceability">
        /// A <see cref="float"/> representing a confidence measure of whether or not the <see cref="Track"/> is danceable.
        /// </param>
        /// <param name="energy">A <see cref="float"/> representing how energetic the <see cref="Track"/> is.</param>
        /// <param name="instrumentalness">
        /// A <see cref="float"/> representing a confidence measure of whether or not the <see cref="Track"/> is instrumental.
        /// </param>
        /// <param name="liveness">
        /// A <see cref="float"/> representing a confidence measure of whether or not the <see cref="Track"/> was performed live.
        /// </param>
        /// <param name="loudness">A <see cref="float"/> representing the loudness of the <see cref="Track"/> in decibels.</param>
        /// <param name="speechiness">A <see cref="float"/> representing the presence of spoken words in the <see cref="Track"/>.</param>
        /// <param name="tempo">A <see cref="float"/> representing the tempo of the <see cref="Track"/> in beats per minute.</param>
        /// <param name="valence">A <see cref="float"/> representing the valence of the <see cref="Track"/>.</param>
        public AudioFeatures(
            string id,
            Uri uri,
            Uri trackHref,
            Uri analysisUrl,
            int duration,
            int timeSignature,
            int key,
            int mode,
            float acousticness,
            float danceability,
            float energy,
            float instrumentalness,
            float liveness,
            float loudness,
            float speechiness,
            float tempo,
            float valence)
            : base(id, uri)
        {
            TrackHref = trackHref;
            AnalysisUrl = analysisUrl;
            Duration = duration;
            TimeSignature = timeSignature;
            Key = key;
            Mode = mode;
            Acousticness = acousticness;
            Danceability = danceability;
            Energy = energy;
            Instrumentalness = instrumentalness;
            Liveness = liveness;
            Loudness = loudness;
            Speechiness = speechiness;
            Tempo = tempo;
            Valence = valence;
        }

        public Uri TrackHref { get; init; }
        public Uri AnalysisUrl { get; init; }
        public int Duration { get; init; }
        public int TimeSignature { get; init; }
        public int Key { get; init; }
        public int Mode { get; init; }
        public float Acousticness { get; init; }
        public float Danceability { get; init; }
        public float Energy { get; init; }
        public float Instrumentalness { get; init; }
        public float Liveness { get; init; }
        public float Loudness { get; init; }
        public float Speechiness { get; init; }
        public float Tempo { get; init; }
        public float Valence { get; init; }
    }
}
