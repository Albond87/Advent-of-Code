public static class Puzzle04
{
    public static int Part1(string[] lines)
    {
        int containments = 0;
        foreach (string l in lines) {
            int[][] elves = l.Split(",").Select(e=>e.Split("-").Select(n=>int.Parse(n)).ToArray()).ToArray();
            if ((elves[0][0] <= elves[1][0] && elves[0][1] >= elves[1][1]) || (elves[1][0] <= elves[0][0] && elves[1][1] >= elves[0][1])) containments++;
        }
        return containments;
    }

    public static int Part2(string[] lines)
    {
        int overlaps = 0;
        foreach (string l in lines) {
            int[][] elves = l.Split(",").Select(e=>e.Split("-").Select(n=>int.Parse(n)).ToArray()).ToArray();
            if ((elves[0][0] >= elves[1][0] && elves[0][0] <= elves[1][1]) || (elves[1][0] >= elves[0][0] && elves[1][0] <= elves[0][1])) overlaps++;
        }
        return overlaps;
    }
}