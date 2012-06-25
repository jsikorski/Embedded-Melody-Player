using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;

namespace EmbeddedMelodyPlayer
{
    public class ProgramController
    {
        private readonly SdFileReader _sdFileReader;
        private readonly MelodyCostructorProvider _melodyCostructorProvider;

        public ProgramController()
        {
            _sdFileReader = new SdFileReader();
            _melodyCostructorProvider = new MelodyCostructorProvider();
        }

        public void Start()
        {
            byte[] melodyData = _sdFileReader.ReadFile("melody.me");
            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();
            Melody melody = melodyConstructor.CreateMelodyFromBytes(melodyData);
            string melodyString = melody.ToString();
        }
    }
}