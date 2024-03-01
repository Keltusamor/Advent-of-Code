namespace _2023;

public class Day02
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(2348, Deserialize([.. File.ReadAllLines("Day02Input.txt")])
            .Where(game => game.Draws.All(IsValidDraw))
            .Sum(game => game.Index));
    }

    private static bool IsValidDraw(Draw draw)
    {
        return draw.Red <= 12
            && draw.Green <= 13
            && draw.Blue <= 14;
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(76008, Deserialize([.. File.ReadAllLines("Day02Input.txt")])
            .Select(game => (
                Red: game.Draws.Max(draw => draw.Red),
                Green: game.Draws.Max(draw => draw.Green),
                Blue: game.Draws.Max(draw => draw.Blue)))
            .Aggregate(0, (acc, x) => acc + (x.Red * x.Green * x.Blue)));
    }

    private class Game
    {
        public int Index { get; set; }
        public List<Draw> Draws { get; set; } = [];
    }

    private class Draw
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    private static List<Game> Deserialize(List<string> input)
    {
        return input.Select(line => line.Split(": ")[1])
            .Select((round, index) => new Game
            {
                Index = index + 1,
                Draws = round.Split("; ")
                    .Select(draw => draw.Split(", ")
                        .Select(x => (
                            Number: int.Parse(x.Split(" ")[0].Trim()),
                            Color: x.Split(" ")[1].Trim())))
                    .Select(draw => new Draw
                    {
                        Red = draw.SingleOrDefault(x => x.Color.Equals("red", StringComparison.OrdinalIgnoreCase)).Number,
                        Green = draw.SingleOrDefault(x => x.Color.Equals("green", StringComparison.OrdinalIgnoreCase)).Number,
                        Blue = draw.SingleOrDefault(x => x.Color.Equals("blue", StringComparison.OrdinalIgnoreCase)).Number
                    }).ToList()
            }).ToList();
    }
}
