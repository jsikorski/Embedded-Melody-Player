using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Reading;
using Microsoft.SPOT;

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
            Debug.Print("Constructing melody...");

            IMelodyConstructor melodyConstructor = _melodyCostructorProvider.GetMelodyConstructor();
            _currentContext.Melody = melodyConstructor.CreateMelodyFromBytes(_currentContext.MelodyData);
        }
    }
}