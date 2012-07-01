using System.Threading;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Pins;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class PlayMelody : ICommand
    {
        private readonly ProgramState _programState;
        private readonly PlayingContext _playingContext;

        public PlayMelody(ProgramState programState, PlayingContext playingContext)
        {
            _programState = programState;
            _playingContext = playingContext;
        }

        public void Execute()
        {
            var playingThread = new Thread(Play);

            Debug.GC(true);
            _playingContext.CanPlay.WaitOne();

            //Debug.Print("Playing melody...");
            playingThread.Start();
        }

        private void Play()
        {
            _programState.IsPlaying = true;
            _playingContext.MelodyFrament.Play(Outputs.BuzzerPwm);
            _programState.IsPlaying = false;

            _playingContext.CanPlay.Set();
        }
    }
}