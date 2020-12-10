using System;

namespace Spotify.ObjectModel
{
    /// <summary>
    /// Represents a section of an <see cref="AudioAnalysis"/> object.
    /// A section is a subdivision of a <see cref="Track"/> that contains a roughly consistent rhythm/timbre throughout its duration.
    /// </summary>
    public record Section : TimeInterval
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Section"/> record with the specified values.
        /// </summary>
        /// <param name="start">A <see cref="float"/> representing the starting point (in seconds) of the section.</param>
        /// <param name="duration">A <see cref="float"/> representing the duration (in seconds) of the section.</param>
        /// <param name="confidence">
        /// A <see cref="float"/> representing the confidence of the reliability of the section's 'designation'.
        /// </param>
        /// <param name="loudness">A <see cref="float"/> representing the overall loudness (in decibels) of the section.</param>
        /// <param name="tempo">A <see cref="float"/> representing the overall tempo (in beats per minute) of the section.</param>
        /// <param name="tempoConfidence">
        /// A <see cref="float"/> representing the confidence of the reliability of the <paramref name="tempo"/> value.
        /// </param>
        /// <param name="key">An <see cref="int"/> representing the estimated overall key of the section.</param>
        /// <param name="keyConfidence">
        /// A <see cref="float"/> representing the confidence of the reliability of the <paramref name="key"/> value.
        /// </param>
        /// <param name="mode">
        /// A <see cref="Modality"/> value representing the modality of the section, or <see langword="null"/> if none is provided.
        /// </param>
        /// <param name="modeConfidence">
        /// A <see cref="float"/> representing the confidence of the reliability of the <paramref name="mode"/> value.
        /// </param>
        /// <param name="timeSignature">An <see cref="int"/> representing the estimated overall time signature of the section.</param>
        /// <param name="timeSignatureConfidence">
        /// A <see cref="float"/> representing the confidence of the reliability of the <paramref name="timeSignature"/> value.
        /// </param>
        public Section(
            float start,
            float duration,
            float confidence,
            float loudness,
            float tempo,
            float tempoConfidence,
            int key,
            float keyConfidence,
            Modality? mode,
            float modeConfidence,
            int timeSignature,
            float timeSignatureConfidence) :
            base(start, duration, confidence)
        {
            this.Loudness = loudness;
            this.Tempo = tempo;
            this.TempoConfidence = tempoConfidence;
            this.Key = key;
            this.KeyConfidence = keyConfidence;
            this.Mode = mode;
            this.ModeConfidence = modeConfidence;
            this.TimeSignature = timeSignature;
            this.TimeSignatureConfidence = timeSignatureConfidence;
        }

        /// <summary>
        /// Gets or sets the overall loudness (in decibels) of the <see cref="Section"/>.
        /// </summary>
        /// <returns>A <see cref="float"/> representing the overall loudness (in decibels) of the <see cref="Section"/>.</returns>
        public float Loudness { get; init; }
        /// <summary>
        /// Gets or sets the overall tempo (in beats per minute) of the <see cref="Section"/>.
        /// </summary>
        /// <returns>A <see cref="float"/> representing the overall tempo (in beats per minute) of the <see cref="Section"/>.</returns>
        public float Tempo { get; init; }
        /// <summary>
        /// Gets or sets the confidence of the reliability of the <see cref="Tempo"/> value, from <c>0.0</c> to <c>1.0</c>.
        /// </summary>
        /// <remarks><c>0.0</c> represents low confidence in the value, while <c>1.0</c> represents high confidence.</remarks>
        /// <returns>A <see cref="float"/> representing the confidence of the reliability of the <see cref="Tempo"/> value.</returns>
        public float TempoConfidence { get; init; }
        /// <summary>
        /// Gets or sets the estimated overall key of the <see cref="Section"/>.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the estimated overall key of the <see cref="Section"/>.</returns>
        public int Key { get; init; }
        /// <summary>
        /// Gets or sets the confidence of the reliability of the <see cref="Key"/> value, from <c>0.0</c> to <c>1.0</c>.
        /// </summary>
        /// <remarks><c>0.0</c> represents low confidence in the value, while <c>1.0</c> represents high confidence.</remarks>
        /// <returns>A <see cref="float"/> representing the confidence of the reliability of the <see cref="Key"/> value.</returns>
        public float KeyConfidence { get; init; }
        /// <summary>
        /// Gets or sets the modality of the <see cref="Section"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Modality"/> value representing the modality of the <see cref="Section"/>,
        /// or <see langword="null"/> if none was provided.
        /// </returns>
        public Modality? Mode { get; init; }
        /// <summary>
        /// Gets or sets the confidence of the reliability of the <see cref="Mode"/> value, from <c>0.0</c> to <c>1.0</c>.
        /// </summary>
        /// <remarks><c>0.0</c> represents low confidence in the value, while <c>1.0</c> represents high confidence.</remarks>
        /// <returns>A <see cref="float"/> representing the confidence of the reliability of the <see cref="Mode"/> value.</returns>
        public float ModeConfidence { get; init; }
        /// <summary>
        /// Gets or sets the estimated overall time signature of the <see cref="Section"/>.
        /// </summary>
        /// <returns>An <see cref="int"/> representing the estimated overall time signature of the <see cref="Section"/>.</returns>
        public int TimeSignature { get; init; }
        /// <summary>
        /// Gets or sets the confidence of the reliability of the <see cref="TimeSignature"/> value, from <c>0.0</c> to <c>1.0</c>.
        /// </summary>
        /// <remarks><c>0.0</c> represents low confidence in the value, while <c>1.0</c> represents high confidence.</remarks>
        /// <returns>A <see cref="float"/> representing the confidence of the reliability of the <see cref="TimeSignature"/> value.</returns>
        public float TimeSignatureConfidence { get; init; }
    }
}