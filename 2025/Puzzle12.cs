public class Puzzle12 : Puzzle
{
    public Puzzle12() : base("12") { }

    public override void Part1()
    {
        int count = 0;
        foreach (string line in inputs[30..])
        {
            int[] dimensions = line.Split(':')[0].Split('x').Select(int.Parse).ToArray();
            int[] shapeCounts = line.Split(": ")[1].Split(' ').Select(int.Parse).ToArray();
            // Check easy case - all pieces fit in a grid of 3x3 regions with no intersecting
            if (dimensions[0]/3 * dimensions[1]/3 >= shapeCounts.Sum()) count++;
        }
        Console.WriteLine(count);
    }

    public override void Part2()
    {
        Console.WriteLine("All puzzles complete!");
    }
}