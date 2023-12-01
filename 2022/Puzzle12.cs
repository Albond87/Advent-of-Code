public static class Puzzle12
{
    static int ShortestPath(int[][] map, int start, int goal) {
        int height = map.Length;
        int width = map[0].Length;
        Dictionary<int, int> unvisited = new Dictionary<int, int>();
        List<int> visited = new List<int>();
        unvisited.Add(start,0);
        while (true) {
            if (unvisited.Count() == 0) return 1000;
            KeyValuePair<int, int> current = unvisited.OrderBy(n=>n.Value).First();
            if (current.Key == goal) return current.Value;
            unvisited.Remove(current.Key);
            visited.Add(current.Key);
            int x=current.Key%width;
            int y=current.Key/width;
            int elevation=map[y][x];
            foreach (int[] offset in new int[][]{new int[]{0,-1},new int[]{-1,0},new int[]{1,0},new int[]{0,1}}) {
                if (current.Key % width == width-1 && offset[0]==1) continue;
                if (current.Key % width == 0 && offset[0]==-1) continue;
                
                int check=current.Key+offset[0]+(width*offset[1]);
                if (check<0 || check>=height*width) continue;
                if (visited.Contains(check)) continue;
                x=check%width;
                y=check/width;
                if (map[y][x] > elevation+1) continue;
                int dist = unvisited.GetValueOrDefault(check,0);
                if (dist==0 || current.Value+1 < dist) {
                    unvisited[check] = current.Value + 1;
                }
            }
        }
    }
    public static int Part1(string[] lines)
    {
        int[][] map = lines.Select(l=>l.ToList().Select(h=>Convert.ToInt32((byte)h)-97).ToArray()).ToArray();
        int start = 0;
        int goal = 0;
        int height = map.Length;
        int width = map[0].Length;
        for (int y=0; y<height; y++) {
            for (int x=0; x<width; x++) {
                if (map[y][x] == -14) {
                    start = y*width+x;
                    map[y][x] = 0;
                }
                else if (map[y][x] == -28) {
                    goal = y*width+x;
                    map[y][x] = 25;
                }
            }
        }
        return ShortestPath(map, start, goal);
    }

    public static int Part2(string[] lines)
    {
        int[][] map = lines.Select(l=>l.ToList().Select(h=>Convert.ToInt32((byte)h)-97).ToArray()).ToArray();
        int goal = 0;
        int height = map.Length;
        int width = map[0].Length;
        for (int y=0; y<height; y++) {
            for (int x=0; x<width; x++) {
                if (map[y][x] == -14) {
                    map[y][x] = 0;
                }
                else if (map[y][x] == -28) {
                    goal = y*width+x;
                    map[y][x] = 25;
                }
            }
        }
        int shortest = 1000;
        for (int y=0; y<height; y++) {
            for (int x=0; x<width; x++) {
                if (map[y][x] == 0) {
                    int path = ShortestPath(map, y*width+x, goal);
                    if (path < shortest) shortest = path;
                }
            }
        }
        return shortest;
    }
}