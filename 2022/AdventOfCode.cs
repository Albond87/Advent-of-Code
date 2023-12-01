class AdventOfCode
{
    static void Main()
    {
        string[] lines = System.IO.File.ReadAllLines(@"Inputs/input01.txt");
        Console.WriteLine(Puzzle01.Solve(lines));
    }
}