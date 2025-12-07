public class Puzzle07 : Puzzle
{
    readonly int startX;
    readonly HashSet<int>[] splitters;

    public Puzzle07() : base("07")
    {
        startX = inputs[0].IndexOf('S');
        splitters = new HashSet<int>[inputs.Length];
        for (int y=1; y<inputs.Length; y++)
        {
            HashSet<int> rowSplitters = inputs[y].Select((c,i)=>c=='^'?i:-1).Where(i=>i>0).ToHashSet();
            splitters[y] = rowSplitters;
        }
    }

    public override void Part1()
    {
        HashSet<int> beams = [startX];
        int splits = 0;
        for (int y=1; y<splitters.Length; y++)
        {
            HashSet<int> nextBeams = [];
            foreach (int b in beams)
            {
                if (splitters[y].Contains(b))
                {
                    nextBeams.Add(b-1);
                    nextBeams.Add(b+1);
                    splits++;
                }
                else
                {
                    nextBeams.Add(b);
                }
            }
            beams = nextBeams;
        }
        Console.WriteLine(splits);
    }

    public override void Part2()
    {
        Dictionary<int, double> timelines = new() { {startX, 1} };
        for (int y=1; y<splitters.Length; y++)
        {
            Dictionary<int, double> nextTimelines = [];
            foreach (int b in timelines.Keys)
            {
                if (splitters[y].Contains(b))
                {
                    nextTimelines[b-1] = nextTimelines.GetValueOrDefault(b-1,0) + timelines[b];
                    nextTimelines[b+1] = nextTimelines.GetValueOrDefault(b+1,0) + timelines[b];
                }
                else
                {
                    nextTimelines[b] = nextTimelines.GetValueOrDefault(b,0) + timelines[b];
                }
            }
            timelines = nextTimelines;
        }
        Console.WriteLine(timelines.Values.Sum());
    }
}