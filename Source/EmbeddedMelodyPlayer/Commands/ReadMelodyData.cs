using EmbeddedMelodyPlayer.Reading;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ReadMelodyData : ICommand
    {
        private readonly CurrentContext _currentContext;
        private readonly SdFilesReader _sdFilesReader;

        public ReadMelodyData(CurrentContext currentContext)
        {
            _currentContext = currentContext;
            _sdFilesReader = new SdFilesReader();
        }

        public void Execute()
        {
            _currentContext.MelodyData = _sdFilesReader.ReadFile("melody.me");
        }
    }
}