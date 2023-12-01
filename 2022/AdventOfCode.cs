class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input06.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input11.txt");
        Console.WriteLine(Puzzle11.Part1(input));
        Console.WriteLine(Puzzle11.Part2(input));
        //Puzzle10.Part2(input);
    }
}