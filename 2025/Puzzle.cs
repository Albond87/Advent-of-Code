public abstract class Puzzle(string day)
{
    protected string input = File.ReadAllText(@"Inputs/input" + day + ".txt").Replace("\r","");
    protected string[] inputs = File.ReadAllLines(@"Inputs/input" + day + ".txt");

    public abstract void Part1();

    public abstract void Part2();
}