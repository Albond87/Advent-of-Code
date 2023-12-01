class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input15.txt");
        Console.WriteLine(Puzzle15.Part1(input));
        Console.WriteLine(Puzzle15.Part2(input));
        //Puzzle10.Part2(input);
    }
}