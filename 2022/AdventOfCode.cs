class AdventOfCode
{
    static void Main()
    {
        string input = System.IO.File.ReadAllText(@"Inputs/input17.txt");
        //string[] input = System.IO.File.ReadAllLines(@"Inputs/input16.txt");
        Console.WriteLine(Puzzle17.Part1(input));
        Console.WriteLine(Puzzle17.Part2(input));
        //Puzzle10.Part2(input);
    }
}