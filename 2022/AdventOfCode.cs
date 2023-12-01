class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input08.txt");
        Console.WriteLine(Puzzle08.Part1(input));
        Console.WriteLine(Puzzle08.Part2(input));
    }
}