using EmbeddedMelodyPlayer.Playing;
using NSubstitute;
using NUnit.Framework;

namespace EmbeddedMelodyPlayer.Tests.Playing
{
    public class MelodyFragmentTests
    {
         [Test]
         public void to_string_returns_empty_for_empty_melody()
         {
             var melody = new MelodyFrament(new MelodyElement[] {}, false, false);

             string expected = string.Empty;
             string actual = melody.ToString();
             Assert.AreEqual(expected, actual);
         }

        [Test]
        public void to_string_returns_aggregation_of_to_string_for_melody_elements()
        {
            var melodyElement1 = Substitute.For<MelodyElement>();
            var melodyElement2 = Substitute.For<MelodyElement>();
            var melodyElement3 = Substitute.For<MelodyElement>();

            melodyElement1.ToString().Returns("C[1]");
            melodyElement2.ToString().Returns("D[2]");
            melodyElement3.ToString().Returns("G[4]");

            var melodyElements = new[] {melodyElement1, melodyElement2, melodyElement3};
            var melody = new MelodyFrament(melodyElements, false, false);

            const string expected = "C[1] D[2] G[4]";
            string actual = melody.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}