class AdventOfCode
{
    static void Main()
    {
        //string lines = System.IO.File.ReadAllText(@"Inputs/input01.txt");
        string[] lines = System.IO.File.ReadAllLines(@"Inputs/input02.txt");
        Console.WriteLine(Puzzle02.Part1(lines));
        Console.WriteLine(Puzzle02.Part2(lines));
    }
}