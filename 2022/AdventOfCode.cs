class AdventOfCode
{
    static void Main()
    {
        //string lines = System.IO.File.ReadAllText(@"Inputs/input01.txt");
        string[] lines = System.IO.File.ReadAllLines(@"Inputs/input03.txt");
        Console.WriteLine(Puzzle03.Part1(lines));
        Console.WriteLine(Puzzle03.Part2(lines));
    }
}