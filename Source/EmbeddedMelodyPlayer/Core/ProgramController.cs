using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Core
{
    public class ProgramController
    {
        public void Start()
        {
            Debug.Print("Program is starting...");

            ICommand startSdDetection = new StartSdDetection(OnSdCardDetected);
            CommandsInvoker.ExecuteCommand(startSdDetection);
        }

        private void OnSdCardDetected(VolumeInfo volumeInfo)
        {
            var playingContext = new PlayingContext(volumeInfo);
            using (var busyScope = new BusyScope(playingContext))
            {
                PlayMelody(playingContext);
            }
        }

        private static void PlayMelody(PlayingContext playingContext)
        {
            while (!playingContext.WasEntireMelodyFileRead)
            {
                PlayingMelodyFragmentPipe playingFragmentPipe =
                    PlayingMelodyFragmentPipe.CreateForContext(playingContext);
                CommandsInvoker.ExecuteCommand(playingFragmentPipe, () => playingContext.FailureDetected = true);

                if (playingContext.FailureDetected)
                    return;
            }

            playingContext.LastMelodyFragmentPlayedEvent.WaitOne();
        }
    }
}