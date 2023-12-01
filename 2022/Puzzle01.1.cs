public static class Puzzle01
{
    public static int Solve(string[] lines)
    {
        var intLines = lines.Select(x => int.Parse(x));
        return intLines.Max() + intLines.Min();
    }
}