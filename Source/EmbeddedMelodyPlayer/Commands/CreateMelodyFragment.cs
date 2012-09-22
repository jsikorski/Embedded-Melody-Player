using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class CreateMelodyFragment : ICommand
    {
        private readonly PlayingContext _playingContext;
        private readonly MelodyFragmentBuilder _melodyFragmentBuilder;

        public CreateMelodyFragment(PlayingContext playingContext)
        {
            _playingContext = playingContext;
            _melodyFragmentBuilder = new MelodyFragmentBuilder();
        }

        public void Execute()
        {
            Debug.Print("Constructing melody fragment...");

            EnsureFragmentCanBeSafetlyOverwritten();
            CreateFragment();
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

        private void CreateFragment()
        {
            bool isItFirstFragment = _playingContext.MelodyFragment == null;
            bool isItLastFragment = _playingContext.WasEntireMelodyFileRead;
            _playingContext.MelodyFragment = _melodyFragmentBuilder.CreateMelodyFragmentFromBytes(
                _playingContext.MelodyFileChunkData, isItFirstFragment, isItLastFragment);
        }
    }
}