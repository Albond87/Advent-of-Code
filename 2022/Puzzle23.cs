public static class Puzzle23
{
    public static int Part1(string[] lines)
    {
        Dictionary<int, HashSet<int>> elves = new Dictionary<int, HashSet<int>>();
        int elfCount = 0;
        for (int y=0; y<lines.Length; y++) {
            elves.Add(y,new HashSet<int>());
            for (int x=0; x<lines[y].Length; x++) {
                if (lines[y][x] == '#') {
                    elves[y].Add(x);
                    elfCount++;
                }
            }
        }
        int[][] dirs = new int[][] {
            new int[] {0,1,2},
            new int[] {5,6,7},
            new int[] {0,3,5},
            new int[] {2,4,7}
        };
        Dictionary<int,int[]> moves = new Dictionary<int, int[]>() {
            {1, new int[]{0,-1}},
            {3, new int[]{-1,0}},
            {4, new int[]{1,0}},
            {6, new int[]{0,1}}
        };
        int dir = 0;
        for (int i=0; i<10; i++) {
            List<int[]> proposed = new List<int[]>();
            foreach (KeyValuePair<int, HashSet<int>> row in elves) {
                int y = row.Key;
                foreach (int x in row.Value) {
                    List<bool> adjacent = new List<bool>();
                    bool move=false;
                    for (int yd = -1; yd < 2; yd++) {
                        if (!elves.ContainsKey(y+yd)) {
                            adjacent.Add(false); adjacent.Add(false); adjacent.Add(false);
                            continue;
                        }
                        for (int xd = -1; xd < 2; xd++) {
                            if (yd == 0 && xd == 0) continue;
                            if (elves[y+yd].Contains(x+xd)) {
                                adjacent.Add(true);
                                move = true;
                            }
                            else adjacent.Add(false);
                        }
                    }
                    if (move) {
                        int check=dir;
                        for (int c=0; c<4; c++) {
                            bool canMove = true;
                            foreach (int p in dirs[check]) {
                                if (adjacent[p]) canMove=false;
                            }
                            if (canMove) {
                                proposed.Add(new int[]{x,y,x+moves[dirs[check][1]][0],y+moves[dirs[check][1]][1]});
                                break;
                            }
                            check = (check+1)%4;
                        }
                    }
                }
            }
            proposed = proposed.GroupBy(p=>p[2]+","+p[3]).Where(g=>g.Count()==1).Select(g=>g.First()).ToList();
            foreach (int[] p in proposed) {
                elves[p[1]].Remove(p[0]);
                HashSet<int> row = elves.GetValueOrDefault(p[3],new HashSet<int>());
                row.Add(p[2]);
                elves[p[3]] = row;
            }
            dir = (dir+1)%4;
        }
        List<int> occupiedRows = elves.Keys.Where(k=>elves[k].Count()>0).Order().ToList();
        int minX = 0;
        int maxX = 0;
        occupiedRows.ForEach(o=>elves[o].ToList().ForEach(x=>{
            if (x < minX) minX = x;
            if (x > maxX) maxX = x;
        }));
        return (maxX-minX+1)*(occupiedRows.Last()-occupiedRows.First()+1)-elfCount;
    }

    public static int Part2(string[] lines)
    {
        Dictionary<int, HashSet<int>> elves = new Dictionary<int, HashSet<int>>();
        int elfCount = 0;
        for (int y=0; y<lines.Length; y++) {
            elves.Add(y,new HashSet<int>());
            for (int x=0; x<lines[y].Length; x++) {
                if (lines[y][x] == '#') {
                    elves[y].Add(x);
                    elfCount++;
                }
            }
        }
        int[][] dirs = new int[][] {
            new int[] {0,1,2},
            new int[] {5,6,7},
            new int[] {0,3,5},
            new int[] {2,4,7}
        };
        Dictionary<int,int[]> moves = new Dictionary<int, int[]>() {
            {1, new int[]{0,-1}},
            {3, new int[]{-1,0}},
            {4, new int[]{1,0}},
            {6, new int[]{0,1}}
        };
        int dir = 0;
        int round = 1;
        while (true) {
            List<int[]> proposed = new List<int[]>();
            foreach (KeyValuePair<int, HashSet<int>> row in elves) {
                int y = row.Key;
                foreach (int x in row.Value) {
                    List<bool> adjacent = new List<bool>();
                    bool move=false;
                    for (int yd = -1; yd < 2; yd++) {
                        if (!elves.ContainsKey(y+yd)) {
                            adjacent.Add(false); adjacent.Add(false); adjacent.Add(false);
                            continue;
                        }
                        for (int xd = -1; xd < 2; xd++) {
                            if (yd == 0 && xd == 0) continue;
                            if (elves[y+yd].Contains(x+xd)) {
                                adjacent.Add(true);
                                move = true;
                            }
                            else adjacent.Add(false);
                        }
                    }
                    if (move) {
                        int check=dir;
                        for (int c=0; c<4; c++) {
                            bool canMove = true;
                            foreach (int p in dirs[check]) {
                                if (adjacent[p]) canMove=false;
                            }
                            if (canMove) {
                                proposed.Add(new int[]{x,y,x+moves[dirs[check][1]][0],y+moves[dirs[check][1]][1]});
                                break;
                            }
                            check = (check+1)%4;
                        }
                    }
                }
            }
            proposed = proposed.GroupBy(p=>p[2]+","+p[3]).Where(g=>g.Count()==1).Select(g=>g.First()).ToList();
            if (proposed.Count() == 0) {
                return round;
            }
            foreach (int[] p in proposed) {
                elves[p[1]].Remove(p[0]);
                HashSet<int> row = elves.GetValueOrDefault(p[3],new HashSet<int>());
                row.Add(p[2]);
                elves[p[3]] = row;
            }
            dir = (dir+1)%4;
            round++;
        }
    }
}