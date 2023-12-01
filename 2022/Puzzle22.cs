public static class Puzzle22
{
    public static int Part1(string[] lines)
    {
        int[][] map = lines.SkipLast(2).Select(l=>l.ToArray().Select(a=>a==' '?0:(a=='.'?1:2)).ToArray()).ToArray();
        List<int> dists = new List<int>(){};
        List<int> turns = new List<int>(){};
        string moves=lines.Last();
        int i=0;
        while (i<moves.Length) {
            string d="";
            while (i<moves.Length && moves[i]!='R' && moves[i]!='L') d+=moves[i++];
            dists.Add(int.Parse(d));
            if (i<moves.Length) turns.Add(moves[i++]=='R'?1:-1);
        }
        int[] pos = new int[]{0,0};
        while (map[0][pos[0]] != 1) pos[0]++;
        int dir=0;

        for (int t=0; t<dists.Count(); t++) {
            for (int m=0; m<dists[t]; m++) {
                int newX = pos[0];
                int newY = pos[1];
                switch (dir) {
                    case 0:
                        newX++;
                        if (newX == map[newY].Length || map[newY][newX] == 0) {
                            newX = 0;
                            while (map[newY][newX] == 0) newX++;
                        }
                        break;
                    case 2:
                        newX--;
                        if (newX == -1 || map[newY][newX] == 0) {
                            newX = map[newY].Length-1;
                            while (map[newY][newX] == 0) newX--;
                        }
                        break;
                    case 1:
                        newY++;
                        if (newY == map.Length || map[newY].Length <= newX || map[newY][newX] == 0) {
                            newY = 0;
                            while (map[newY].Length <= newX || map[newY][newX] == 0) newY++;
                        }
                        break;
                    case 3:
                        newY--;
                        if (newY == -1 || map[newY].Length <= newX || map[newY][newX] == 0) {
                            newY = map.Length-1;
                            while (map[newY].Length <= newX || map[newY][newX] == 0) newY--;
                        }
                        break;
                }
                if (map[newY][newX] == 2) break;
                pos[0] = newX;
                pos[1] = newY;
            }
            if (t<turns.Count()) dir = (dir+turns.ElementAt(t)+4) % 4;
        }
        return (1000*(pos[1]+1)) + (4*(pos[0]+1)) + dir;
    }

    public static int Part2(string[] lines)
    {
        int[][][] faces = new int[][][] {
            lines.Take(50).Select(l=>l.ToArray().Skip(50).Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
            lines.Take(50).Select(l=>l.ToArray().Skip(100).Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
            lines.Skip(50).Take(50).Select(l=>l.ToArray().Skip(50).Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
            lines.Skip(100).Take(50).Select(l=>l.ToArray().Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
            lines.Skip(100).Take(50).Select(l=>l.ToArray().Skip(50).Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
            lines.Skip(150).Take(50).Select(l=>l.ToArray().Take(50).Select(a=>a=='.'?0:1).ToArray()).ToArray(),
        };
        Dictionary<int,Dictionary<int,int[]>> wrappings = new Dictionary<int, Dictionary<int, int[]>>() {
            {0, new Dictionary<int, int[]>() {
                {0, new int[] {1,0}},
                {1, new int[] {2,1}},
                {2, new int[] {3,0}},
                {3, new int[] {5,0}}
            }},
            {1, new Dictionary<int, int[]>() {
                {0, new int[] {4,2}},
                {1, new int[] {2,2}},
                {2, new int[] {0,2}},
                {3, new int[] {5,3}}
            }},
            {2, new Dictionary<int, int[]>() {
                {0, new int[] {1,3}},
                {1, new int[] {4,1}},
                {2, new int[] {3,1}},
                {3, new int[] {0,3}}
            }},
            {3, new Dictionary<int, int[]>() {
                {0, new int[] {4,0}},
                {1, new int[] {5,1}},
                {2, new int[] {0,0}},
                {3, new int[] {2,0}}
            }},
            {4, new Dictionary<int, int[]>() {
                {0, new int[] {1,2}},
                {1, new int[] {5,2}},
                {2, new int[] {3,2}},
                {3, new int[] {2,3}}
            }},
            {5, new Dictionary<int, int[]>() {
                {0, new int[] {4,3}},
                {1, new int[] {1,1}},
                {2, new int[] {0,1}},
                {3, new int[] {3,3}}
            }}
        };
        List<int> dists = new List<int>(){};
        List<int> turns = new List<int>(){};
        string moves=lines.Last();
        int i=0;
        while (i<moves.Length) {
            string d="";
            while (i<moves.Length && moves[i]!='R' && moves[i]!='L') d+=moves[i++];
            dists.Add(int.Parse(d));
            if (i<moves.Length) turns.Add(moves[i++]=='R'?1:-1);
        }
        int[][] dirs = new int[][] {
            new int[] {1,0},
            new int[] {0,1},
            new int[] {-1,0},
            new int[] {0,-1}
        };
        int face = 0;
        int[] pos = new int[]{0,0};
        int dir=0;

        for (int t=0; t<dists.Count(); t++) {
            for (int m=0; m<dists[t]; m++) {
                int newX = pos[0] + dirs[dir][0];
                int newY = pos[1] + dirs[dir][1];
                int newFace = face;
                int newDir = dir;                
                if (newX == -1 || newX == 50 || newY == -1 || newY == 50) {
                    int[] wrap = wrappings[face][dir];
                    newFace = wrap[0];
                    newDir = wrap[1];
                    switch (dir) {
                        case 0:
                            switch (wrap[1]) {
                                case 0:
                                    newX = 0;
                                    break;
                                case 1:
                                    newX = 49-newY;
                                    newY = 0;
                                    break;
                                case 2:
                                    newX = 49;
                                    newY = 49-newY;
                                    break;
                                case 3: 
                                    newX = newY;
                                    newY = 49;
                                    break;
                            }
                            break;
                        case 1:
                            switch (wrap[1]) {
                                case 0:
                                    newY = newX;
                                    newX = 0;
                                    break;
                                case 1:
                                    newY = 0;
                                    break;
                                case 2:
                                    newY = newX;
                                    newX = 49;
                                    break;
                                case 3:
                                    newX = 49-newX;
                                    newY = 49;
                                    break;
                            }
                            break;
                        case 2: 
                            switch (wrap[1]) {
                                case 0:
                                    newX = 0;
                                    newY = 49-newY;
                                    break;
                                case 1:
                                    newX = newY;
                                    newY = 0;
                                    break;
                                case 2: 
                                    newX = 49;
                                    break;
                                case 3:
                                    newX = 49-newY;
                                    newY = 49;
                                    break;
                            }
                            break;
                        case 3:
                            switch (wrap[1]) {
                                case 0:
                                    newY = newX;
                                    newX = 0;
                                    break;
                                case 2:
                                    newY = 49-newX;
                                    newX = 49;
                                    break;
                                case 3:
                                    newY = 49;
                                    break;
                            }
                            break;
                    }
                }
                if (faces[newFace][newY][newX] == 1) break;
                pos[0] = newX;
                pos[1] = newY;
                face = newFace;
                dir = newDir;
            }
            if (t<turns.Count()) dir = (dir+turns.ElementAt(t)+4) % 4;
        }
        switch (face) {
            case 0:
                pos[0] += 51;
                pos[1] += 1;
                break;
            case 1: 
                pos[0] += 101;
                pos[1] += 1;
                break;
            case 2: 
                pos[0] += 51;
                pos[1] += 51;
                break;
            case 3: 
                pos[0] += 1;
                pos[1] += 101;
                break;
            case 4: 
                pos[0] += 51;
                pos[1] += 101;
                break;
            case 5: 
                pos[0] += 1;
                pos[1] += 151;
                break;
        }
        return (1000*(pos[1])) + (4*(pos[0])) + dir;
    }
}