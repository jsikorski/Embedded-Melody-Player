using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ConstructMelody : ICommand
    {
        private readonly PlayingContext _playingContext;
        private readonly MelodyCostructorProvider _melodyCostructorProvider;

        public ConstructMelody(PlayingContext playingContext)
        {
            _playingContext = playingContext;
            _melodyCostructorProvider = new MelodyCostructorProvider();
        }

        public void Execute()
        {
            Debug.Print("Constructing melody...");

            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();
            _playingContext.MelodyFrament = melodyConstructor.CreateMelodyFromBytes(_playingContext.MelodyFileChunkData);
        }
    }
}