public class Puzzle18 : Puzzle
{
    readonly int width = 70;
    readonly int height = 70;
    readonly int initialbytes = 1024;
    readonly Dictionary<(int,int),int> bytes = [];

    public Puzzle18() : base("18") {
        bytes = inputs.Select((b,i) => new KeyValuePair<(int,int),int>((int.Parse(b.Split(",")[0]),int.Parse(b.Split(",")[1])),i+1)).ToDictionary();
    }

    // Return the length of the shortest path from (0,0) to (width,height) at the given time, or -1 if impossible
    int ShortestPath(int time) {
        Dictionary<(int,int),int> shortest = new() { [(0,0)]=0 };
        Queue<(int,int)> frontier = new([(0,0)]);
        HashSet<(int,int)> explored = [];
        int shortestpath = -1;
        while (shortestpath == -1 && frontier.Count > 0) {
            var current = frontier.Dequeue();
            var currentdist = shortest[current];
            explored.Add(current);
            List<(int,int)> checks = [];
            if (current.Item1 > 0) {
                checks.Add((current.Item1-1,current.Item2));
            }
            if (current.Item1 < width) {
                checks.Add((current.Item1+1,current.Item2));
            }
            if (current.Item2 > 0) {
                checks.Add((current.Item1,current.Item2-1));
            }
            if (current.Item2 < height) {
                checks.Add((current.Item1,current.Item2+1));
            }
            foreach (var check in checks) {
                if (check == (width,height)) {
                    shortestpath = currentdist + 1;
                    break;
                }
                if (bytes.TryGetValue(check, out var bytetime)) {
                    if (bytetime <= time) {
                        continue;
                    }
                }
                if (!explored.Contains(check)) {
                    if (shortest.TryGetValue(check, out var checkshortest)) {
                        if (currentdist + 1 < checkshortest) {
                            shortest[check] = currentdist + 1;
                        }
                    }
                    else {
                        shortest[check] = currentdist + 1;
                        frontier.Enqueue(check);
                    }
                }
            }
            
        }
        return shortestpath;
    }

    public override void Part1()
    {
        Console.WriteLine(ShortestPath(initialbytes));
    }

    public override void Part2()
    {
        // Binary search for the first byte which blocks the exit
        int lowest = 0;
        int highest = bytes.Count;
        while (highest-lowest > 1) {
            int midpoint = lowest + (highest-lowest)/2;
            if (ShortestPath(midpoint) == -1) {
                highest = midpoint;
            }
            else {
                lowest = midpoint;
            }
        }
        var blockingbyte = bytes.First(b=>b.Value==highest).Key; 
        Console.WriteLine(blockingbyte.Item1 + "," + blockingbyte.Item2);
    }
}