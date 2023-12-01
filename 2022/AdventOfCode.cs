class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input09.txt");
        Console.WriteLine(Puzzle09.Part1(input));
        Console.WriteLine(Puzzle09.Part2(input));
    }
}