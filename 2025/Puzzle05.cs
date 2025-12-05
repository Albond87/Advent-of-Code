public class Puzzle05 : Puzzle
{
    readonly double[][] idRanges;
    readonly double[] ids;

    public Puzzle05() : base("05")
    {
        idRanges = inputs.TakeWhile(i=>i!="").Select(r=>r.Split('-').Select(double.Parse).ToArray()).ToArray();
        ids = inputs[(idRanges.Length+1)..].Select(double.Parse).ToArray();
    }

    public override void Part1()
    {
        int count = 0;
        foreach (double id in ids)
        {
            foreach (var idRange in idRanges)
            {
                if (id >= idRange[0] && id <= idRange[1])
                {
                    count++;
                    break;
                }
            }
        }
        Console.WriteLine(count);
    }

    public override void Part2()
    {
        List<double[]> mergedRanges = [];
        foreach (var idRange in idRanges)
        {
            double start = idRange[0];
            double stop = idRange[1];
            bool inserted = false;
            for (int i=0; i<mergedRanges.Count; i++)
            {
                double[] current = mergedRanges[i];
                if (start > current[1]) continue; // Range starts later than current one ends
                if (stop < current[0])
                {
                    // Range starts after previous one and ends before current one
                    // No overlap so can insert directly
                    mergedRanges.Insert(i, [start, stop]);
                    inserted = true;
                    break;
                }
                if (start < current[0])
                {
                    // New range overlaps start of current one - update current start
                    mergedRanges[i][0] = start;
                }
                if (stop > current[1])
                {
                    // New range overlaps end of current one
                    
                    // Check forward through higher ranges in case it overlaps multiple
                    int j=i+1;
                    while (j < mergedRanges.Count)
                    {
                        if (stop < mergedRanges[j][0])
                        {
                            // Range stops before next one starts - no overlap
                            break;
                        }
                        if (stop <= mergedRanges[j][1])
                        {
                            // Range stops within next one - merge the higher one into the current one
                            stop = mergedRanges[j][1];
                            mergedRanges.RemoveAt(j);
                            break;
                        }
                        // Otherwise range fully overlaps next one so remove the overlap
                        mergedRanges.RemoveAt(j);
                        j++;
                    }
                    // Update current end
                    mergedRanges[i][1] = stop;
                }
                inserted = true;
                break;
            }
            if (!inserted)
            {
                mergedRanges.Add([start, stop]);
            }
        }
        double count = mergedRanges.Select(r=>r[1]-r[0]+1).Sum();
        Console.WriteLine(count);
    }
}