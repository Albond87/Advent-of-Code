public class Puzzle07 : Puzzle
{
    public Puzzle07() : base("07") { }

    static bool EquationPossible(List<long> numbers, long target, bool concat, int pos=0, long result=0) {
        if (pos == numbers.Count) return result==target;
        if (result > target) return false;
        if (pos == 0) return EquationPossible(numbers, target, concat, 1, numbers[0]);
        if (EquationPossible(numbers, target, concat, pos+1, result+numbers[pos])) return true;
        if (EquationPossible(numbers, target, concat, pos+1, result*numbers[pos])) return true;
        if (concat) return EquationPossible(numbers, target, concat, pos+1, long.Parse(result.ToString()+numbers[pos].ToString()));
        return false;
    }

    public override void Part1()
    {
        long total = 0;
        foreach (var line in inputs) {
            var numbers = line.Split([": "," "],StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            if (EquationPossible(numbers[1..], numbers[0], false)) {
                total += numbers[0];
            }
        }
        Console.WriteLine(total);
    }

    public override void Part2()
    {
        long total = 0;
        foreach (var line in inputs) {
            var numbers = line.Split([": "," "],StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            if (EquationPossible(numbers[1..], numbers[0], true)) {
                total += numbers[0];
            }
        }
        Console.WriteLine(total);
    }
}