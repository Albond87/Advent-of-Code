class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input14.txt");
        Console.WriteLine(Puzzle14.Part1(input));
        Console.WriteLine(Puzzle14.Part2(input));
        //Puzzle10.Part2(input);
    }
}