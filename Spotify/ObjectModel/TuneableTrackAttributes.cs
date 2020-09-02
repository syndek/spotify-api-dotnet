using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a collection of values used in calls to <see cref="ISpotifyBrowseApi.GetRecommendationsAsync"/>.
    /// </summary>
    /// <seealso href="https://spotify.dev/documentation/web-api/reference/browse/get-recommendations/#tuneable-track-attributes"/>
    public record TuneableTrackAttributes : Object
    {
        /// <summary>
        /// Gets or sets the popularity of the track.
        /// Popularity values will be between <c>0</c> and <c>100</c>, with <c>100</c> being the most popular.
        /// Popularity is calculated by algorithm and is based, for the most part,
        /// on the total number of plays the track has had and how recently those plays are.
        /// </summary>
        /// <returns>
        /// An <see cref="Int32"/> representing the popularity of the track, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Int32? Popularity { get; init; }
        /// <summary>
        /// Gets or sets the duration of the track in milliseconds.
        /// </summary>
        /// <returns>
        /// An <see cref="Int32"/> representing the duration of the track in milliseconds,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Int32? Duration { get; init; }
        /// <summary>
        /// Gets or sets the overall estimated time signature of the track.
        /// The time signature (meter) is a notational convention to specify how many beats are in each bar (or measure).
        /// </summary>
        /// <returns>
        /// An <see cref="Int32"/> representing the time signature of the track, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Int32? TimeSignature { get; init; }
        /// <summary>
        /// Gets or sets the key of the track.
        /// Numbers map to pitches using standard <see href="https://en.wikipedia.org/wiki/Pitch_class">pitch class notation</see>.
        /// </summary>
        /// <returns>
        /// An <see cref="Int32"/> representing the key the track is in using standard pitch class notation,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Int32? Key { get; init; }
        /// <summary>
        /// Gets or sets the modality of the track.
        /// Modality represents the type of scale (either major or minor) from which the track's melodic content is derived.
        /// </summary>
        /// <returns>
        /// A <see cref="Modality"/> value representing the modality of the track, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Modality? Mode { get; init; }
        /// <summary>
        /// Gets or sets the acousticness of the track, from <c>0.0</c> to <c>1.0</c>.
        /// <c>0.0</c> represents low confidence that the track is acoustic, while <c>1.0</c> represents high confidence.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing a confidence measure of whether or not the track is acoustic,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Acousticness { get; init; }
        /// <summary>
        /// Gets or sets the danceability of the track, from <c>0.0</c> to <c>1.0</c>.
        /// Danceability describes how suitable a track is for dancing based on a combination of musical elements.
        /// <c>0.0</c> represents a track with low danceability, while <c>1.0</c> represents a highly danceable track.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing a confidence measure of whether or not the track is danceable,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Danceability { get; init; }
        /// <summary>
        /// Gets or sets the energy of the track, from <c>0.0</c> to <c>1.0</c>.
        /// Energy represents a peceptual measure of intensity and activity.
        /// Typically, an energetic track will feel fast, loud, and noisy.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing how energetic the track is, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Energy { get; init; }
        /// <summary>
        /// Gets or sets the instrumentalness of the track, from <c>0.0</c> to <c>1.0</c>.
        /// "Ooh" and "aah" sounds are treated as instrumental in this context, while rap or spoken words are clearly 'vocal'.
        /// The closer the value is to <c>1.0</c>, the greater the likelihood the track contains no vocal content.
        /// Values above <c>0.5</c> are intended to represent instrumental tracks.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing a confidence measure of whether or not the track is instrumental,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Instrumentalness { get; init; }
        /// <summary>
        /// Gets or sets the liveness of the track, from <c>0.0</c> to <c>1.0</c>.
        /// Liveness represents the presence of an audience in the recording, thus representing the probability that a track was performed live.
        /// <c>0.0</c> represents low probability that the track was performed live, while <c>1.0</c> represents a high probability.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing a confidence measure of whether or not the track was performed live,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Liveness { get; init; }
        /// <summary>
        /// Gets or sets the loudness of the track.
        /// The loudness is represented by decibels (dB) and are averaged across the entire track.
        /// Values typically range from -60dB to 0dB.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing the loudness of the track in decibels, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Loudness { get; init; }
        /// <summary>
        /// Gets or sets the speechiness of the track, from <c>0.0</c> to <c>1.0</c>.
        /// Speechiness represents the presence of spoken words in a track.
        /// The more exclusively speech-like the recording, the closer to <c>1.0</c> the value will be.
        /// Values above <c>0.66</c> represent tracks that are probably made entirely of spoken words.
        /// Values between <c>0.66</c> and <c>0.33</c> represent tracks that may contains both music and speech, either in sections or layered.
        /// Values below <c>0.33</c> most likely represent music and other non-speech-like tracks.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing the presence of spoken words in the track,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Speechiness { get; init; }
        /// <summary>
        /// Gets or sets the overall estimated tempo of the track.
        /// Tempo is recorded in beats per minute (BPM) and represents the speed or pace of a given piece.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing the tempo of the track in beats per minute,
        /// or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Tempo { get; init; }
        /// <summary>
        /// Gets or sets the valence of the track, from <c>0.0</c> to <c>1.0</c>.
        /// Valence represents the musical positiveness conveyed by a track.
        /// <c>0.0</c> represents a track that sounds more negative, while <c>1.0</c> represents a track that sounds more positive.
        /// </summary>
        /// <returns>
        /// A <see cref="Single"/> representing the valence of the track, or <see langword="null"/> if a value is not provided.
        /// </returns>
        public Single? Valence { get; init; }
    }
}