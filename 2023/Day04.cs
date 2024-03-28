namespace _2023;

public class Day04
{
    private record Scratchcard(int GameIndex, string[] WinningNumbers, string[] ActualNumbers)
    {
        public string[] MyWinningNumbers { get; } = WinningNumbers.Intersect(ActualNumbers).ToArray();
    }

    [Fact]
    public void Part1()
    {
        var scratchcardValues = File.ReadLines("Day04Input.txt")
            .Select(DeserializeScratchcard)
            .Select(scratchcard => scratchcard.MyWinningNumbers)
            .Select(winners => winners.Length == 0 ? 0 : Math.Pow(2, winners.Length - 1));

        Assert.Equal(22193, scratchcardValues.Sum());
    }
    
    private static Scratchcard DeserializeScratchcard(string line)
    {
        var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        
        var tmp = line.Split(":", options);
        var gameIndex = int.Parse(tmp[0].Split(" ", options)[1]);
        var numbers = tmp[1].Split("|", options);
        
        return new Scratchcard(
            gameIndex,
            [.. numbers[0].Split(" ", options)],
            [.. numbers[1].Split(" ", options)]);
    }

    [Fact]
    public void Part2()
    {
        var scratchcards = File.ReadLines("Day04Input.txt")
            .Select(DeserializeScratchcard)
            .ToList();

        var copies = CalcNumberOfCopies(scratchcards, scratchcards);

        Assert.Equal(5625994, scratchcards.Count + copies.Count());
    }

    private static IEnumerable<Scratchcard> CalcNumberOfCopies(IEnumerable<Scratchcard> allCards, IEnumerable<Scratchcard> copies)
    {
        List<Scratchcard> result = [];

        foreach (var copy in copies)
        {
            var start = copy.GameIndex - 1 + 1;
            var end = start + copy.MyWinningNumbers.Length;
            result.AddRange(allCards.Skip(start).Take(copy.MyWinningNumbers.Length));
        }

        return result.Count == 0 ? [] : result.Concat(CalcNumberOfCopies(allCards, result));
    }
}
