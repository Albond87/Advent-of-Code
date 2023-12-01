public abstract class Puzzle(string day)
{
    protected string input = System.IO.File.ReadAllText(@"Inputs/input" + day + ".txt").Replace("\r","");
    protected string[] inputs = System.IO.File.ReadAllLines(@"Inputs/input" + day + ".txt");

    public abstract void Part1();

    public abstract void Part2();
}