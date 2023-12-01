class AdventOfCode
{
    static void Main()
    {
        string lines = System.IO.File.ReadAllText(@"Inputs/input05.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"Inputs/input05.txt");
        Console.WriteLine(Puzzle05.Part1(lines));
        Console.WriteLine(Puzzle05.Part2(lines));
    }
}