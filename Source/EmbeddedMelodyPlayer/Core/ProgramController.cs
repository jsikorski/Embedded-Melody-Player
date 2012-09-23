using System;
using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Utils;
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
            CommandsInvoker.Execute(startSdDetection);
        }

        private void OnSdCardDetected(VolumeInfo volumeInfo)
        {
            var playingContext = new PlayingContext(volumeInfo);
            using (new BusyScope(playingContext))
            {
                PlayMelody(playingContext);
            }
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