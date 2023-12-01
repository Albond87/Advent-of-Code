class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input10.txt");
        Console.WriteLine(Puzzle10.Part1(input));
        //Console.WriteLine(Puzzle10.Part2(input));
        Puzzle10.Part2(input);
    }
}