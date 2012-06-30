using System.Threading;
using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using GHIElectronics.NETMF.IO;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer
{
    public class ProgramController
    {
        private CurrentContext _currentContext;

        public void Start()
        {
            Debug.Print("Program is starting...");
            _currentContext = new CurrentContext();

            ICommand startSdDetection = new StartSdDetection(TryPlay, _currentContext);
            CommandsInvoker.ExecuteCommand(startSdDetection);
        }

        private void TryPlay()
        {
            var playingPipe = new CommandsPipe(new ICommand[]
                                                   {
                                                       new ReadMelodyData(_currentContext),
                                                       new ConstructMelody(_currentContext),
                                                       new PlayMelody(_currentContext)
                                                   });

            CommandsInvoker.ExecuteCommand(playingPipe);
        }
    }
}