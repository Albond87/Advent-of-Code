public static class Puzzle01
{
    public static int Part1(string input)
    {
        return input.Split("\n\n").Select(e=>e.Split("\n").Select(c=>int.Parse(c)).Sum()).Max();
        // int max = 0;
        // int count = 0;

        // foreach (string line in lines)
        // {
        //     if (line.Equals("")) {
        //         max = count>max ? count : max;
        //         count = 0;
        //     } else {
        //         count += int.Parse(line);
        //     }
        // }
        // return max;
    }

    public static int Part2(string input)
    {
        return input.Split("\n\n").Select(e=>e.Split("\n").Select(c=>int.Parse(c)).Sum()).OrderDescending().Take(3).Sum();

        // List<int> counts = new List<int>();
        // int count = 0;

        // foreach (string line in lines)
        // {
        //     if (line.Equals("")) {
        //         counts.Add(count);
        //         count = 0;
        //     } else {
        //         count += int.Parse(line);
        //     }
        // }
        // return counts.OrderDescending().Take(3).Sum();
    }
}