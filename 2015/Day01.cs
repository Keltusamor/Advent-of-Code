namespace _2015
{
    public class Day01
    {
        [Fact]
        public void Part1()
        {
            Assert.Equal(232, _Part1(File.ReadAllText("Day01Input.txt")));
        }

        private static int _Part1(string input)
        {
            return input.Aggregate(0, (acc, c) => acc + GoUpOrDown(c));
        }

        private static int GoUpOrDown(char c)
        {
            return c == '(' ? 1 : -1;
        }

        [Fact]
        public void Part2()
        {
            Assert.Equal(1783, _Part2(File.ReadAllText("Day01Input.txt")));
        }

        private static int _Part2(string input)
        {
            var floor = 0;

            for (var i = 0; i < input.Length; i++)
            {
                floor += GoUpOrDown(input[i]);
                if (floor == -1)
                {
                    return i + 1;
                }
            }

            throw new InvalidOperationException("Input can't reach basement.");
        }
    }
}
