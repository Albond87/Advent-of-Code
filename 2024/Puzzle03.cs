using System.Text.RegularExpressions;

public class Puzzle03 : Puzzle
{
    readonly Regex regex;

    public Puzzle03() : base("03") {
        regex = new Regex(@"mul\([0-9]+,[0-9]+\)");
    }

    public override void Part1()
    {
        var ops = regex.Matches(input).ToArray();
        int sum = 0;
        foreach (var op in ops) {
            var nums = op.Value[4..^1].Split(",").Select(int.Parse).ToArray();
            sum += nums[0] * nums[1];
        }
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        string enabled = "";
        int pos = 0;
        while (pos > -1) {
            int nextpos = input.IndexOf("don't()", pos);
            if (nextpos > -1) {
                enabled += input.Substring(pos, nextpos-pos);
                pos = input.IndexOf("do()", nextpos);
            }
            else {
                enabled += input.Substring(pos);
                pos = -1;
            }
        }
        var ops = regex.Matches(enabled).ToArray();
        int sum = 0;
        foreach (var op in ops) {
            var nums = op.Value[4..^1].Split(",").Select(int.Parse).ToArray();
            sum += nums[0] * nums[1];
        }
        Console.WriteLine(sum);
    }
}