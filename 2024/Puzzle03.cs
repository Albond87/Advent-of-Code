using System.Text.RegularExpressions;

public class Puzzle03 : Puzzle
{
    public Puzzle03() : base("03") { }

    public override void Part1()
    {
        Regex regex = new Regex(@"mul\([0-9]+,[0-9]+\)");
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
        // Original solution, finding don't() and do() one at a time
        // string enabled = "";
        // int pos = 0;
        // while (pos > -1) {
        //     int nextpos = input.IndexOf("don't()", pos);
        //     if (nextpos > -1) {
        //         enabled += input.Substring(pos, nextpos-pos);
        //         pos = input.IndexOf("do()", nextpos);
        //     }
        //     else {
        //         enabled += input.Substring(pos);
        //         pos = -1;
        //     }
        // }
        // input = enabled;
        // Part1();

        // Improved solution, using a single combined regex
        Regex regex = new Regex(@"mul\([0-9]+,[0-9]+\)|do\(\)|don't\(\)");
        var ops = regex.Matches(input).ToArray();
        int sum = 0;
        bool enabled = true;
        foreach (var op in ops) {
            if (op.Value == "do()") {
                enabled = true;
            }
            else if (op.Value == "don't()") {
                enabled = false;
            }
            else if (enabled) {
                var nums = op.Value[4..^1].Split(",").Select(int.Parse).ToArray();
                sum += nums[0] * nums[1];    
            }
        }
        Console.WriteLine(sum);
    }
}