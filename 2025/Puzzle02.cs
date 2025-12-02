public class Puzzle02 : Puzzle
{
    readonly List<long[]> ranges;

    public Puzzle02() : base("02")
    {
        ranges = input.Split(',').Select(r => new long[2]{long.Parse(r.Split('-')[0]), long.Parse(r.Split('-')[1])}).ToList();
    }

    public override void Part1()
    {
        long invalidSum = 0;
        foreach (var r in ranges)
        {
            int lowerLength = r[0].ToString().Length;
            int upperLength = r[1].ToString().Length;

            for (int l=lowerLength; l<=upperLength; l++)
            {
                if (l % 2 != 0) continue; // Only check even numbered lengths
                // Attempt at pruning the search, not really needed as brute force is quick
                long divider = long.Parse("1" + new string('0', l/2-1) + "1"); // e.g. 11, 101, 1001
                long rangeMin = l==lowerLength ? r[0] : divider * (long)Math.Pow(10, l/2-1); // e.g. 11, 1010, 100100
                long rangeMax = l==upperLength ? r[1] : (long)Math.Pow(10, l) - 1;
                
                for (long id=rangeMin; id<=rangeMax; id++)
                {
                    string idStr = id.ToString();
                    if (idStr[..(l / 2)].Equals(idStr[(l / 2)..])) invalidSum += id;
                }
            }
        }
        Console.WriteLine(invalidSum);
    }

    public override void Part2()
    {
        long invalidSum = 0;
        foreach (var r in ranges)
        {
            for (long id=r[0]; id<=r[1]; id++)
            {
                string idStr = id.ToString();
                int idLen = idStr.Length;
                for (int l=1; l<=idLen/2; l++)
                {
                    if (idLen % l != 0) continue;
                    if (idStr.Equals(string.Concat(Enumerable.Repeat(idStr[..l], idLen/l))))
                    {
                        invalidSum += id;
                        break;
                    }
                }
            }
        }
        Console.WriteLine(invalidSum);
    }
}