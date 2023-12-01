class AdventOfCode
{
    static void Main()
    {
        string lines = System.IO.File.ReadAllText(@"Inputs/input01.txt");
        Console.WriteLine(Puzzle01.Part1(lines));
        Console.WriteLine(Puzzle01.Part2(lines));
    }
}