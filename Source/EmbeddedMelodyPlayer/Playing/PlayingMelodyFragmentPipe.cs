﻿using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;

namespace EmbeddedMelodyPlayer.Playing
{
    public class PlayingMelodyFragmentPipe : CommandsPipe
    {
        private PlayingMelodyFragmentPipe()
        {
        }

        public static PlayingMelodyFragmentPipe CreateForContext(PlayingContext playingContext)
        {
            ICommand readMelodyFileChunk = new ReadMelodyFileChunk(playingContext.SdCardVolume, playingContext);
            ICommand constructMelodyFragment = new CreateMelodyFragment(playingContext);
            ICommand playMelodyFragment = new PlayMelodyFragment(playingContext);

            var commands = new[] {readMelodyFileChunk, constructMelodyFragment, playMelodyFragment};
            return new PlayingMelodyFragmentPipe {_commands = commands};
        }
    }
}