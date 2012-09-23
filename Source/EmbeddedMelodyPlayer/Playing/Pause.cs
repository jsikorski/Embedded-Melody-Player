using System.Threading;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Pause : MelodyElement
    {
        public Pause(int duration)
        {
            CheckElementParameters('P', duration);

            Symbol = 'P';
            Duration = duration;
        }

        public override void Play()
        {
            Thread.Sleep(MelodyElementDurationResolver.GetElementDuration(this));
        }
    }
}