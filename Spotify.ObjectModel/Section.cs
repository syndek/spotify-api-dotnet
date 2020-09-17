using System;

namespace Spotify.ObjectModel
{
    public record Section : TimeInterval
    {
        public Section(
            Single start,
            Single duration,
            Single confidence,
            Single loudness,
            Single tempo,
            Single tempoConfidence,
            Int32 key,
            Single keyConfidence,
            Modality? mode,
            Single modeConfidence,
            Int32 timeSignature,
            Single timeSignatureConfidence) :
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

        public Single Loudness { get; init; }
        public Single Tempo { get; init; }
        public Single TempoConfidence { get; init; }
        public Int32 Key { get; init; }
        public Single KeyConfidence { get; init; }
        public Modality? Mode { get; init; }
        public Single ModeConfidence { get; init; }
        public Int32 TimeSignature { get; init; }
        public Single TimeSignatureConfidence { get; init; }
    }
}