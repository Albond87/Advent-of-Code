public class Puzzle06 : Puzzle
{
    public Puzzle06() : base("06") { }

    public override void Part1()
    {
        List<List<double>> numbers = [];
        foreach (var row in inputs[..^1])
        {
            List<double> rowNums = row.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToList();
            for (int c=0; c<rowNums.Count; c++)
            {
                if (numbers.Count < c+1) numbers.Add([rowNums[c]]);
                else numbers[c].Add(rowNums[c]);
            }
        }
        double resultSum = inputs[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select((op, i) => op.Equals("+") ? numbers[i].Sum() : numbers[i].Aggregate(1d, (a,v)=>a*v)).Sum();
        Console.WriteLine(resultSum);
    }

    public override void Part2()
    {
        string[] nums = inputs[..^1];
        string operators = inputs[^1];
        int column = inputs[0].Length-1;
        List<double> operands = [];
        double resultSum = 0;
        while (column >= 0)
        {
            operands.Add(double.Parse(string.Concat(nums.Select(r=>r[column]))));
            if (operators[column] != ' ')
            {
                if (operators[column] == '+')
                {
                    resultSum += operands.Sum();
                }
                else
                {
                    resultSum += operands.Aggregate(1d, (a,v)=>a*v);
                }
                column -= 2;
                operands = [];
            }
            else
            {
                column--;
            }
        }
        Console.WriteLine(resultSum);
    }
}