public class Puzzle01 : Puzzle
{
    List<int> left;
    List<int> right;

    public Puzzle01() : base("01") {
        left = inputs.Select(i => int.Parse(i.Split("   ")[0])).ToList();
        right = inputs.Select(i => int.Parse(i.Split("   ")[1])).ToList();
    }

    public override void Part1()
    {        
        left.Sort();
        right.Sort();
        int distance = left.Select((n,i) => Math.Abs(n-right[i])).Sum();
        Console.WriteLine(distance);
    }

    public override void Part2()
    {
        int similarity = left.Select(l => l * right.Where(r => r==l).Count()).Sum();
        Console.WriteLine(similarity);
    }
}