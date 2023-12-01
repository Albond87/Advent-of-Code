class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input17.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input25.txt");
        Console.WriteLine(Puzzle25.Part1(input));
        Puzzle25.Part2();
        //Puzzle10.Part2(input);
    }
}