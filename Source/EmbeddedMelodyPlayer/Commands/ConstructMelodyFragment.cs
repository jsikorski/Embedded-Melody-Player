using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ConstructMelodyFragment : ICommand
    {
        private readonly MelodyCostructorProvider _melodyCostructorProvider;
        private readonly PlayingContext _playingContext;

        public ConstructMelodyFragment(PlayingContext playingContext)
        {
            _playingContext = playingContext;
            _melodyCostructorProvider = new MelodyCostructorProvider();
        }

        #region ICommand Members

        public void Execute()
        {
            Debug.Print("Constructing melody fragment...");

            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();
            _playingContext.MelodyFrament = melodyConstructor.CreateMelodyFragmentFromBytes(
                _playingContext.MelodyFileChunkData, _playingContext.WasEntireMelodyFileRead);
        }

        #endregion
    }
}