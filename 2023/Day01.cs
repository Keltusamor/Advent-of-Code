using System.Text.RegularExpressions;

namespace _2023;

public partial class Day01
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(52974, _Part1(File.ReadAllText("Day01Input.txt")));
    }

    private static int _Part1(string input)
    {
        return FindFirstAndLastNumberFromZeroToNine()
            .Matches(input)
            .Select(match => string.Concat(match.Groups["first"].Value, match.Groups["last"].Value))
            .Select(int.Parse)
            .Sum();
    }

    // First group has to be a lookahead because input might have only one number so it should not be consumed
    [GeneratedRegex("(?=(?'first'[0-9])).*(?'last'[0-9])")]
    private static partial Regex FindFirstAndLastNumberFromZeroToNine();

    [Fact]
    public void Part2()
    {
        Assert.Equal(53340, _Part2(File.ReadAllText("Day01Input.txt")));
    }

    private static int _Part2(string input)
    {
        return FindFirstAndLastNumberFromZeroToNineIncludingWords()
            .Matches(input)
            .Select(match => string.Concat(
                ConvertWordToNumber(match.Groups["first"].Value),
                ConvertWordToNumber(match.Groups["last"].Value)))
            .Select(int.Parse)
            .Sum();
    }

    private static string ConvertWordToNumber(string number)
    {
        return number switch
        {
            "0" or "zero" => "0",
            "1" or "one" => "1",
            "2" or "two" => "2",
            "3" or "three" => "3",
            "4" or "four" => "4",
            "5" or "five" => "5",
            "6" or "six" => "6",
            "7" or "seven" => "7",
            "8" or "eight" => "8",
            "9" or "nine" => "9",
            _ => throw new InvalidOperationException($"Invalid number {number}")
        };
    }

    // First group has to be a lookahead because input might have only one number so it should not be consumed
    [GeneratedRegex("(?=(?'first'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)).*(?'last'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)")]
    private static partial Regex FindFirstAndLastNumberFromZeroToNineIncludingWords();
}
