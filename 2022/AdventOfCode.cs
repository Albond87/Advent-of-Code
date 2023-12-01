class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input07.txt");
        Console.WriteLine(Puzzle07.Part1(input));
        Console.WriteLine(Puzzle07.Part2(input));
    }
}