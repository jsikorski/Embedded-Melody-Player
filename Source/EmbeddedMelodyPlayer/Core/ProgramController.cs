using System.Threading;
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

            ICommand startSdDetection = new StartSdDetection(TryPlay, _programState);
            CommandsInvoker.ExecuteCommand(startSdDetection);
        }

        private void TryPlay()
        {
            var playingContext = new PlayingContext();

            while (!playingContext.WasEntireMelodyFileRead)
            {
                var playingPipe = CreatePlayingPipe(playingContext);
                CommandsInvoker.ExecuteCommand(playingPipe);
            }
        }

        private CommandsPipe CreatePlayingPipe(PlayingContext playingContext)
        {
            ICommand readMelodyData = new ReadMelodyData(_programState.SdCardVolume, playingContext);
            ICommand constructMelody = new ConstructMelody(playingContext);
            ICommand playMelody = new PlayMelody(_programState, playingContext);

            return new CommandsPipe(new[]
                                        {
                                            readMelodyData, 
                                            constructMelody, 
                                            playMelody, 
                                        });
        }
    }
}