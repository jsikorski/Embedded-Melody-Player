using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ConstructMelody : ICommand
    {
        private readonly CurrentContext _currentContext;
        private readonly MelodyCostructorProvider _melodyCostructorProvider;

        public ConstructMelody(CurrentContext currentContext)
        {
            _currentContext = currentContext;
            _melodyCostructorProvider = new MelodyCostructorProvider();
        }

        public void Execute()
        {
            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();
            _currentContext.Melody = melodyConstructor.CreateMelodyFromBytes(_currentContext.MelodyData);
        }
    }
}