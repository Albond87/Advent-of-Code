public class Puzzle20 : Puzzle
{
    readonly int width;
    readonly int height;
    readonly HashSet<(int,int)> walls = [];
    readonly (int,int) startpos;
    readonly (int,int) endpos;
    Dictionary<(int,int),int> distances = [];

    public Puzzle20() : base("20") {
        height = inputs.Length;
        width = inputs[0].Length;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (inputs[y][x] == '#') {
                    walls.Add((x,y));
                }
                else if (inputs[y][x] == 'S') {
                    startpos = (x,y);
                }
                else if (inputs[y][x] == 'E') {
                    endpos = (x,y);
                }
            }
        }
        ExploreMaze();
    }

    // Find the distance from the start to each point along the path to the end
    void ExploreMaze() {
        int x = startpos.Item1;
        int y = startpos.Item2;
        int dir = -1;
        int dist = 0;
        distances[(x,y)] = dist;
        while ((x,y) != endpos) {
            if (dir != 0 && !walls.Contains((x-1,y))) {
                x--;
                dir = 1;
            }
            else if (dir != 1 && !walls.Contains((x+1,y))) {
                x++;
                dir = 0;
            }
            else if (dir != 2 && !walls.Contains((x,y-1))) {
                y--;
                dir = 3;
            }
            else if (dir != 3 && !walls.Contains((x,y+1))) {
                y++;
                dir = 2;
            }
            dist++;
            distances[(x,y)] = dist;
        }
    }

    public override void Part1()
    {
        int count = 0;
        foreach (var p in distances) {
            int x = p.Key.Item1;
            int y = p.Key.Item2;
            int dist = p.Value;
            // Try going through one wall in each direction
            (int,int)[] cheats = [(x-2,y),(x+2,y),(x,y-2),(x,y+2)];
            foreach (var cheat in cheats) {
                if (!walls.Contains(cheat)) {
                    if (distances.TryGetValue(cheat, out int cheatdist)) {
                        // If going through the wall cuts at least 100 steps
                        if (cheatdist - dist - 2 >= 100) {
                            count++;
                        }
                    }
                }
            }
        }
        Console.WriteLine(count);
    }

    public override void Part2()
    {
        int count = 0;
        foreach (var p in distances) {
            int x = p.Key.Item1;
            int y = p.Key.Item2;
            int dist = p.Value;
            // Try 'jumping' to each point within a radius of 20
            for (int ycheat = -20; ycheat <= 20; ycheat++) {
                for (int xcheat = -20 + Math.Abs(ycheat); xcheat <= 20 - Math.Abs(ycheat); xcheat++) {
                    (int,int) cheat = (x+xcheat, y+ycheat);
                    if (!walls.Contains(cheat)) {
                        if (distances.TryGetValue(cheat, out int cheatdist)) {
                            // If jumping to that point cuts at least 100 steps
                            if (cheatdist - dist - Math.Abs(ycheat) - Math.Abs(xcheat) >= 100) {
                                count++;
                            }
                        }
                    }
                }
            }
        }
        Console.WriteLine(count);
    }
}