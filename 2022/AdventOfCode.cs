class AdventOfCode
{
    static void Main()
    {
        //string lines = System.IO.File.ReadAllText(@"Inputs/input01.txt");
        string[] lines = System.IO.File.ReadAllLines(@"Inputs/input04.txt");
        Console.WriteLine(Puzzle04.Part1(lines));
        Console.WriteLine(Puzzle04.Part2(lines));
    }
}