using System.Text.RegularExpressions;

namespace _2023;

public partial class Day03
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex FindAllNumbers();

    [GeneratedRegex(@"[^\d|.]")]
    private static partial Regex IsPartNumber();

    [GeneratedRegex(@"[\*]")]
    private static partial Regex IsStar();

    private record NumberMatch(int Value, int LineNumber, int Start, int End);

    [Fact]
    public void Part1()
    {
        var schematic = File.ReadAllLines("Day03Input.txt");

        var validPartNumbers = FindAllNumbers(schematic)
            .Where(number => IsValidPartNumber(schematic, number))
            .Select(number => number.Value);

        Assert.Equal(539590, validPartNumbers.Sum());
    }

    private static IEnumerable<NumberMatch> FindAllNumbers(string[] input)
    {
        return input.SelectMany((line, lineNumber) =>
            FindAllNumbers()
                .Matches(line)
                .Select(match => new NumberMatch(int.Parse(match.Value), lineNumber, match.Index, match.Index + match.Length))
        );
    }

    private static bool IsValidPartNumber(string[] schematic, NumberMatch number)
    {
        var maxLength = schematic[0].Length;
        var start = Math.Clamp(number.Start - 1, 0, maxLength);
        var end = Math.Clamp(number.End + 1, 0, maxLength);

        var searchArea = (number.LineNumber > 0 ? schematic[number.LineNumber - 1][start..end] : string.Empty)
                       + (number.LineNumber + 1 < schematic.Length ? schematic[number.LineNumber + 1][start..end] : string.Empty)
                       + schematic[number.LineNumber][start..(start + 1)]
                       + schematic[number.LineNumber][(end - 1)..end];

        return IsPartNumber().IsMatch(searchArea);
    }

    [Fact]
    public void Part2()
    {
        var schematic = File.ReadAllLines("Day03Input.txt");

        var numbers = FindAllNumbers(schematic);

        var gears = FindPotentialGears(schematic, numbers)
            .Where(kvp => kvp.Value.Count == 2)
            .Select(kvp => kvp.Value);

        Assert.Equal(80703636, gears.Select(x => x[0] * x[1]).Sum());
    }

    private static Dictionary<string, List<int>> FindPotentialGears(string[] schematic, IEnumerable<NumberMatch> numbers)
    {
        var potentialGears = new Dictionary<string, List<int>>();

        foreach (var number in numbers)
        {
            var maxLength = schematic[0].Length;
            var start = Math.Clamp(number.Start - 1, 0, maxLength);
            var end = Math.Clamp(number.End + 1, 0, maxLength);

            List<int> lineNumberToSearch = [number.LineNumber];
            if (number.LineNumber > 0) lineNumberToSearch.Add(number.LineNumber - 1);
            if (number.LineNumber + 1 < schematic.Length) lineNumberToSearch.Add(number.LineNumber + 1);

            foreach (var lineNumber in lineNumberToSearch)
            {
                var searchArea = schematic[lineNumber][start..end];
                var match = IsStar().Match(searchArea);
                if (match.Success)
                {
                    var key = $"{lineNumber}, {start + match.Index}";
                    if (!potentialGears.TryGetValue(key, out List<int>? value))
                    {
                        value = ([]);
                        potentialGears.Add(key, value);
                    }
                    value.Add(number.Value);
                }
            }
        }

        return potentialGears;
    }
}
