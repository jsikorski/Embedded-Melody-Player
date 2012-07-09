using System.Threading;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class PlayMelodyFragment : ICommand
    {
        private readonly PlayingContext _playingContext;
        private readonly Thread _playingThread;
        private MelodyFrament _melodyFragment;

        public PlayMelodyFragment(PlayingContext playingContext)
        {
            _playingContext = playingContext;
            _playingThread = new Thread(() => PlayFragment(_melodyFragment));
        }

        #region ICommand Members

        public void Execute()
        {
            RememberMelodyFragment();
            StartPlaying();
        }

        #endregion

        private void RememberMelodyFragment()
        {
            _melodyFragment = _playingContext.MelodyFragment;
            _playingContext.PreviousMelodyFragmentRememberedEvent.Set();
        }

        private void StartPlaying()
        {
            if (!_melodyFragment.IsFirst)
                _playingContext.PreviousMelodyFragmentPlayedEvent.WaitOne();

            _playingThread.Start();
        }

        private void PlayFragment(MelodyFrament melodyFragment)
        {
            Debug.Print("Playing melody fragment...");
            melodyFragment.Play();
            _playingContext.PreviousMelodyFragmentPlayedEvent.Set();

            if (_melodyFragment.IsLast)
                _playingContext.LastMelodyFragmentPlayedEvent.Set();
        }
    }
}