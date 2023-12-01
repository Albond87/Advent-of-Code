public static class Puzzle24
{
    static int width;
    static int height;

    struct Blizzard {
        public Blizzard(int[] position, int[] move) {
            this.position = position;
            this.move = move;
        }
        public int[] position;
        public int[] move;
    }

    static List<Blizzard> moveBlizzards(List<Blizzard> blizzards) {
        List<Blizzard> newBlizzards = new List<Blizzard>();
        foreach (Blizzard bl in blizzards) {
            Blizzard b = new Blizzard(new int[]{bl.position[0],bl.position[1]},bl.move);
            b.position[0] += b.move[0];
            if (b.position[0] == 0) b.position[0] = width-1;
            else if (b.position[0] == width) b.position[0] = 1;
            else {
                b.position[1] += b.move[1];
                if (b.position[1] == 0) b.position[1] = height-1;
                else if (b.position[1] == height) b.position[1] = 1;
            }
            newBlizzards.Add(b);
        }
        return newBlizzards;
    }
    
    static bool[] getMoves(int x, int y, List<Blizzard> blizzards) {
        bool[] moves = new bool[]{true,true,true,true,true};
        if (y==0) {
            moves[0] = false;
            moves[2] = false;
            moves[3] = false;
        }
        else if (y==height) {
            moves[0] = false;
            moves[1] = false;
            moves[2] = false;
        }
        else {
            if (x==1) moves[2] = false;
            else if (x==width-1) moves[0] = false;
            if (y==1 && x!=1) moves[3] = false;
            else if (y==height-1 && x!=width-1) moves[1] = false;
        }
        foreach (Blizzard b in blizzards) {
            if (b.position[0]==x && b.position[1]==y) moves[4] = false;
            if (b.position[1]==y) {
                if (b.position[0]==x+1) moves[0] = false;
                else if (b.position[0]==x-1) moves[2] = false;
            }
            else if (b.position[0]==x) {
                if (b.position[1]==y+1) moves[1] = false;
                else if (b.position[1]==y-1) moves[3] = false;
            }
        }
        return moves;
    }

    static int shortestPath(ref Dictionary<string,int> cache, ref Dictionary<int,List<Blizzard>> bcache, int x, int y, int time, List<Blizzard> blizzards, int limit) {
        if (y==height) return time;
        if (time>limit) return -1;
        string key=x+","+y+","+time;
        if (cache.ContainsKey(key)) return cache[key];
        List<Blizzard> newBlizzards;
        if (bcache.ContainsKey(time)) newBlizzards = bcache[time];
        else {
            newBlizzards = moveBlizzards(blizzards);
            bcache[time] = newBlizzards;
        }
        bool[] moves = getMoves(x,y,newBlizzards);
        if (!moves.Contains(true)) return -1;
        int min=-1;
        if (moves[0]) {
            int right=shortestPath(ref cache,ref bcache,x+1,y,time+1,newBlizzards,limit);
            if (right != -1) {
                if (right < min || min==-1) min=right;
            }   
        }
        if (moves[1]) {
            int down=shortestPath(ref cache,ref bcache,x,y+1,time+1,newBlizzards,limit);
            if (down != -1) {
                if (down < min || min==-1) min=down;
            }
        }
        if (moves[2]) {
            int left=shortestPath(ref cache,ref bcache,x-1,y,time+1,newBlizzards,limit);
            if (left != -1) {
                if (left < min || min==-1) min=left;
            }
        }
        if (moves[3]) {
            int up=shortestPath(ref cache,ref bcache,x,y-1,time+1,newBlizzards,limit);
            if (up != -1) {
                if (up < min || min==-1) min=up;
            }
        }
        if (moves[4] && min==-1) {
            int stay=shortestPath(ref cache,ref bcache,x,y,time+1,newBlizzards,limit);
            if (stay != -1) {
                if (stay < min || min==-1) min=stay;
            }
        }
        cache[key] = min;
        return min;
    }

    static int shortestPathBack(ref Dictionary<string,int> cache, ref Dictionary<int,List<Blizzard>> bcache, int x, int y, int time, List<Blizzard> blizzards, int limit) {
        if (y==0) return time;
        if (time>limit) return -1;
        string key=x+","+y+","+time;
        if (cache.ContainsKey(key)) return cache[key];
        List<Blizzard> newBlizzards;
        if (bcache.ContainsKey(time)) newBlizzards = bcache[time];
        else {
            newBlizzards = moveBlizzards(blizzards);
            bcache[time] = newBlizzards;
        }
        bool[] moves = getMoves(x,y,newBlizzards);
        if (!moves.Contains(true)) return -1;
        int min=-1;
        if (moves[2]) {
            int left=shortestPathBack(ref cache,ref bcache,x-1,y,time+1,newBlizzards,limit);
            if (left != -1) {
                if (left < min || min==-1) min=left;
            }
        }
        if (moves[3]) {
            int up=shortestPathBack(ref cache,ref bcache,x,y-1,time+1,newBlizzards,limit);
            if (up != -1) {
                if (up < min || min==-1) min=up;
            }
        }
        if (moves[0]) {
            int right=shortestPathBack(ref cache,ref bcache,x+1,y,time+1,newBlizzards,limit);
            if (right != -1) {
                if (right < min || min==-1) min=right;
            }   
        }
        if (moves[1]) {
            int down=shortestPathBack(ref cache,ref bcache,x,y+1,time+1,newBlizzards,limit);
            if (down != -1) {
                if (down < min || min==-1) min=down;
            }
        }        
        if (moves[4] && min==-1) {
            int stay=shortestPathBack(ref cache,ref bcache,x,y,time+1,newBlizzards,limit);
            if (stay != -1) {
                if (stay < min || min==-1) min=stay;
            }
        }
        cache[key] = min;
        return min;
    }

    public static int Part1(string[] lines)
    {
        List<Blizzard> blizzards = new List<Blizzard>();
        width = lines[0].Length-1;
        height = lines.Length-1;
        for (int y=1; y<height; y++) {
            for (int x=1; x<width; x++) {
                if (lines[y][x] != '.') {
                    switch (lines[y][x]) {
                        case '>':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{1,0}));
                            break;
                        case '<':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{-1,0}));
                            break;
                        case 'v':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{0,1}));
                            break;
                        case '^':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{0,-1}));
                            break;
                    }
                }
            }
        }
        Dictionary<string,int> cache = new Dictionary<string, int>();
        Dictionary<int,List<Blizzard>> bcache = new Dictionary<int, List<Blizzard>>();
        return shortestPath(ref cache,ref bcache,1,0,0,blizzards,285);
    }

    public static int Part2(string[] lines)
    {
        List<Blizzard> blizzards = new List<Blizzard>();
        width = lines[0].Length-1;
        height = lines.Length-1;
        for (int y=1; y<height; y++) {
            for (int x=1; x<width; x++) {
                if (lines[y][x] != '.') {
                    switch (lines[y][x]) {
                        case '>':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{1,0}));
                            break;
                        case '<':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{-1,0}));
                            break;
                        case 'v':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{0,1}));
                            break;
                        case '^':
                            blizzards.Add(new Blizzard(new int[]{x,y},new int[]{0,-1}));
                            break;
                    }
                }
            }
        }
        Dictionary<string,int> cache = new Dictionary<string, int>();
        Dictionary<int,List<Blizzard>> bcache = new Dictionary<int, List<Blizzard>>();
        int there=shortestPath(ref cache,ref bcache,1,0,0,blizzards,285);
        cache = new Dictionary<string, int>();
        blizzards = bcache[there-1];
        bcache = new Dictionary<int, List<Blizzard>>();
        int back=shortestPathBack(ref cache,ref bcache,width-1,height,0,blizzards,254);
        cache = new Dictionary<string, int>();
        blizzards = bcache[back-1];
        bcache = new Dictionary<int, List<Blizzard>>();
        int thereAgain=shortestPath(ref cache,ref bcache,1,0,0,blizzards,278);
        return there+back+thereAgain;
    }
}