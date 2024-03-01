using System.Text.RegularExpressions;

namespace _2023;

public partial class Day01
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(52974, Deserialize(File.ReadAllText("Day01Input.txt"), FindFirstAndLastNumberFromZeroToNine())
            .Sum(x => x.CombinedNumber));
    }

    // First group has to be a lookahead because input might have only one number so it should not be consumed
    [GeneratedRegex("(?=(?'first'[0-9])).*(?'last'[0-9])", RegexOptions.Multiline)]
    private static partial Regex FindFirstAndLastNumberFromZeroToNine();

    [Fact]
    public void Part2()
    {
        var entries = Deserialize(File.ReadAllText("Day01Input.txt"), FindFirstAndLastNumberFromZeroToNineIncludingWords());
        entries.ForEach(entry =>
        {
            entry.FirstNumber = ConvertWordToNumber(entry.FirstNumber);
            entry.LastNumber = ConvertWordToNumber(entry.LastNumber);
        });
        Assert.Equal(53340, entries.Sum(x => x.CombinedNumber));
    }

    // First group has to be a lookahead because input might have only one number so it should not be consumed
    [GeneratedRegex("(?=(?'first'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)).*(?'last'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Multiline)]
    private static partial Regex FindFirstAndLastNumberFromZeroToNineIncludingWords();

    private static string ConvertWordToNumber(string number)
    {
        return number switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => number
        };
    }

    private class Entry
    {
        public string FirstNumber { get; set; } = string.Empty;
        public string LastNumber { get; set; } = string.Empty;
        public int CombinedNumber => int.Parse(FirstNumber + LastNumber);
    }

    private static List<Entry> Deserialize(string input, Regex regex)
    {
        return regex
            .Matches(input)
            .Select(match => new Entry
            {
                FirstNumber = match.Groups["first"].Value,
                LastNumber = match.Groups["last"].Value
            })
            .ToList();
    }
}
