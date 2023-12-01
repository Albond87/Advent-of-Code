public class Puzzle01 : Puzzle
{
    public Puzzle01() : base("01") { }

    public override void Part1()
    {
        Console.WriteLine(input.Split("\n\n").Select(e=>e.Split("\n").Select(c=>int.Parse(c)).Sum()).Max());
    }

    public override void Part2()
    {
        Console.WriteLine(input.Split("\n\n").Select(e=>e.Split("\n").Select(c=>int.Parse(c)).Sum()).OrderDescending().Take(3).Sum());
    }
}