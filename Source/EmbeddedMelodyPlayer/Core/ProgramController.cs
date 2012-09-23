using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Core
{
    public class ProgramController
    {
        private readonly ProgramContext _programContext;

        public ProgramController()
        {
            _programContext = new ProgramContext();
        }

        public void Start()
        {
            Debug.Print("Program is starting...");

            ICommand startSdDetection = new StartSdDetection(_programContext, Play);
            CommandsInvoker.Execute(startSdDetection);
        }

        private void Play(VolumeInfo volumeInfo)
        {
            _programContext.IsPlaying = true;

            var playingContext = new PlayingContext(volumeInfo);
            using (new BusyScope(playingContext))
            {
                PlayMelody(playingContext);
            }

            _programContext.IsPlaying = false;
        }

        private static void PlayMelody(PlayingContext playingContext)
        {
            while (!playingContext.WasEntireMelodyFileRead)
            {
                var playingFragmentPipe = PlayingMelodyFragmentPipe.CreateForContext(playingContext);
                CommandsInvoker.Execute(playingFragmentPipe, () => playingContext.FailureDetected = true);

                if (playingContext.FailureDetected)
                    return;
            }

            playingContext.LastMelodyFragmentPlayedEvent.WaitOne();
        }
    }
}