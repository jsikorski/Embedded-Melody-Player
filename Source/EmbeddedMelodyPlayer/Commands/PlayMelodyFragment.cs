using System.Threading;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class PlayMelodyFragment : ICommand
    {
        private readonly PlayingContext _playingContext;
        private readonly ProgramState _programState;

        public PlayMelodyFragment(ProgramState programState, PlayingContext playingContext)
        {
            _programState = programState;
            _playingContext = playingContext;
        }

        #region ICommand Members

        public void Execute()
        {
            var playingThread = new Thread(PlayFragment);
            _playingContext.CanPlay.WaitOne();

            Debug.Print("Playing melody fragment...");
            playingThread.Start();
        }

        #endregion

        private void PlayFragment()
        {
            _playingContext.MelodyFrament.Play();
            _playingContext.CanPlay.Set();

            if (_playingContext.MelodyFrament.IsItLastFragment)
                _playingContext.WasLastMelodyFragmentPlayed.Set();
        }
    }
}