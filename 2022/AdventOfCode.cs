﻿class AdventOfCode
{
    static void Main()
    {
        //string input = System.IO.File.ReadAllText(@"Inputs/input17.txt");
        string[] input = System.IO.File.ReadAllLines(@"Inputs/input21.txt");
        Console.WriteLine(Puzzle21.Part1(input));
        Console.WriteLine(Puzzle21.Part2(input));
        //Puzzle10.Part2(input);
    }
}