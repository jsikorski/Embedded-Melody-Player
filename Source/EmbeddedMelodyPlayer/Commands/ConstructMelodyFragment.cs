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

        public void Execute()
        {
            Debug.Print("Constructing melody fragment...");

            EnsureFragmentCanBeSafetlyOverwritten();
            ConstructFragment();
        }

        private void EnsureFragmentCanBeSafetlyOverwritten()
        {
            if (!IsItFirstMelodyFragment())
                _playingContext.PreviousMelodyFragmentRememberedEvent.WaitOne();
        }

        private bool IsItFirstMelodyFragment()
        {
            return _playingContext.MelodyFragment == null;
        }

        private void ConstructFragment()
        {
            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();

            bool isItFirstFragment = _playingContext.MelodyFragment == null;
            bool isItLastFragment = _playingContext.WasEntireMelodyFileRead;
            _playingContext.MelodyFragment = melodyConstructor.CreateMelodyFragmentFromBytes(
                _playingContext.MelodyFileChunkData, isItFirstFragment, isItLastFragment);
        }
    }
}