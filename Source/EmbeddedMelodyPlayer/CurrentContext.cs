using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer
{
    public class CurrentContext
    {
        public byte[] MelodyData { get; set; }
        public Melody Melody { get; set; }
    }
}