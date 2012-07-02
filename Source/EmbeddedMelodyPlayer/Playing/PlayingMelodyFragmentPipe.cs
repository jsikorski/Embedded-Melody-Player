using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;

namespace EmbeddedMelodyPlayer.Playing
{
    public class PlayingMelodyFragmentPipe : CommandsPipe
    {
        private PlayingMelodyFragmentPipe()
        {
        }

        public static PlayingMelodyFragmentPipe CreateForContext(PlayingContext playingContext,
                                                                 ProgramState programState)
        {
            ICommand readMelodyFileChunk = new ReadMelodyFileChunk(programState.SdCardVolume, playingContext);
            ICommand constructMelodyFragment = new ConstructMelodyFragment(playingContext);
            ICommand playMelodyFragment = new PlayMelodyFragment(programState, playingContext);

            var commands = new[] {readMelodyFileChunk, constructMelodyFragment, playMelodyFragment};
            return new PlayingMelodyFragmentPipe {_commands = commands};
        }
    }
}