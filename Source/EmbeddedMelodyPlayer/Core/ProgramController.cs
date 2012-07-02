using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Core
{
    public class ProgramController
    {
        private ProgramState _programState;

        public void Start()
        {
            Debug.Print("Program is starting...");
            _programState = new ProgramState();

            ICommand startSdDetection = new StartSdDetection(Play, _programState);
            CommandsInvoker.ExecuteCommand(startSdDetection);
        }

        private void Play()
        {
            var playingContext = new PlayingContext();

            using (_programState.BusyScope = new BusyScope())
            {
                while (!playingContext.WasEntireMelodyFileRead)
                {
                    PlayingMelodyFragmentPipe playingPipe = PlayingMelodyFragmentPipe.CreateForContext(playingContext,
                                                                                                       _programState);
                    CommandsInvoker.ExecuteCommand(playingPipe);
                }

                playingContext.WasLastMelodyFragmentPlayed.WaitOne();
            }
        }
    }
}