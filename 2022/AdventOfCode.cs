class AdventOfCode
{
    static void Main()
    {
        string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        //string[] input = System.IO.File.ReadAllLines(@"Inputs/input06.txt");
        Console.WriteLine(Puzzle06.Part1(input));
        Console.WriteLine(Puzzle06.Part2(input));
    }
}