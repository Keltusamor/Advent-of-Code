using System.Text.RegularExpressions;

namespace _2023;

public partial class Day01
{
    [Fact]
    public void Part1()
    {
        var number = File.ReadAllLines("Day01Input.txt")
            .Select(CombineFirstAndLastDigit)
            .Sum();
        Assert.Equal(52974, number);
    }

    private static int CombineFirstAndLastDigit(string line)
    {
        var first = line.First(char.IsDigit).ToString();
        var last = line.Last(char.IsDigit).ToString();
        return int.Parse(first + last);
    }

    [Fact]
    public void Part2()
    {
        var number = File.ReadAllLines("Day01Input.txt")
            .Select(CombineFirstAndLastDigitIncludingWords)
            .Sum();
        Assert.Equal(53340, number);
    }

    private static int CombineFirstAndLastDigitIncludingWords(string line)
    {
        var match = FindFirstAndLastNumberFromZeroToNineIncludingWords().Matches(line)[0];
        var first = Map(match.Groups["first"].Value);
        var last = Map(match.Groups["last"].Value);
        return int.Parse(first + last);
    }

    // First group has to be a lookahead because some lines contain only one number and others contain overlapping numbers like 'twone'
    [GeneratedRegex(@"(?=(?'first'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)).*(?'last'[0-9]|zero|one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Multiline)]
    private static partial Regex FindFirstAndLastNumberFromZeroToNineIncludingWords();

    private static string Map(string number) => number switch
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
