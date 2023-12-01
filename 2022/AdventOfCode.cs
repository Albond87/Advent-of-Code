class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input13.txt");
        Console.WriteLine(Puzzle13.Part1(input));
        Console.WriteLine(Puzzle13.Part2(input));
        //Puzzle10.Part2(input);
    }
}