using System;
using System.IO;
using Trivia;
using Xunit;

namespace trivia
{
    public class GameTest
    {
        [Fact]
        public void CharacterizationTest()
        {
            // runs 10.000 "random" games to see the output of old and new code matches  
            for (int seed = 1; seed < 10_000; seed++)
            {
                TestSeed(seed, false);
            }
        }

        private static void TestSeed(int seed, bool printExpected)
        {
            string expectedOutput = ExtractOutput(new Random(seed), new Game());
            if (printExpected)
            {
                Console.WriteLine(expectedOutput);
            }
            string actualOutput = ExtractOutput(new Random(seed), new GameBetter());
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact(Skip = "enable back and set a particular seed to see the output")]
        public void OneSeed()
        {
            TestSeed(1, true);
        }

        private static string ExtractOutput(Random rand, IGame aGame)
        {
            var oldOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);

                aGame.Add("Chet");
                aGame.Add("Pat");
                aGame.Add("Sue");

                bool notAWinner = false;
                do
                {
                    aGame.Roll(rand.Next(5) + 1);

                    if (rand.Next(9) == 7)
                    {
                        notAWinner = aGame.WrongAnswer();
                    }
                    else
                    {
                        notAWinner = aGame.WasCorrectlyAnswered();
                    }

                } while (notAWinner);

                Console.SetOut(oldOut);
                return writer.ToString();
            }
        }
    }
}
