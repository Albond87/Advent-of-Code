class AdventOfCode
{
    static void Main()
    {
        string lines = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        //string[] lines = System.IO.File.ReadAllLines(@"Inputs/input06.txt");
        Console.WriteLine(Puzzle06.Part1(lines));
        Console.WriteLine(Puzzle06.Part2(lines));
    }
}