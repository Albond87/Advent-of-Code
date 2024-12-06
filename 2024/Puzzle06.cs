public class Puzzle06 : Puzzle
{
    int[][] map;
    readonly int startx, starty;
    readonly (int,int)[] directions = [(0, -1), (1,0), (0, 1), (-1, 0)];

    public Puzzle06() : base("06") {
        map = inputs.Select(i => i.ToList().Select(j => j=='.' ? 0 : (j=='#' ? 1 : 2)).ToArray()).ToArray();
        for (int i = 0; i < map.Length; i++) {
            if (Array.IndexOf(map[i], 2) != -1) {
                startx = Array.IndexOf(map[i], 2);
                starty = i;
            }
        }
    }

    // Return the number of unique positions and whether it enters a loop
    (int, bool) SimulateGuard(int x, int y) {
        int count = 1;
        int dir = 0;
        HashSet<(int, int, int)> turnPositions = [];
        while (true) {
            int newx = x + directions[dir].Item1;
            int newy = y + directions[dir].Item2;
            if (newx >= 0 && newx < map[0].Length && newy >= 0 && newy < map.Length) {
                if (map[newy][newx] == 1) {
                    if (turnPositions.Contains((x,y,dir))) {
                        // Has entered a loop
                        return (count, true);
                    }
                    turnPositions.Add((x,y,dir));
                    dir = (dir+1) % 4;
                }
                else {
                    if (map[newy][newx] != 2) {
                        map[newy][newx] = 2;
                        count++;
                    }
                    x = newx;
                    y = newy;
                }
            }
            else {
                // Off the edge of the map
                break;
            }        
        }
        return (count, false);
    }

    public override void Part1()
    {   
        Console.WriteLine(SimulateGuard(startx, starty).Item1);
    }

    public override void Part2()
    {
        int count = 0;
        for (int i = 0; i < map.Length; i++) {
            for (int j = 0; j < map[0].Length; j++) {
                if ((i == starty && j == startx) || (map[i][j] == 1)) {
                    continue;
                }
                map[i][j] = 1;
                if (SimulateGuard(startx, starty).Item2) {
                    count++;
                }
                map[i][j] = 0;
            }
        }
        Console.WriteLine(count);
    }
}