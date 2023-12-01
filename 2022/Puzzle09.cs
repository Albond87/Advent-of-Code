public static class Puzzle09
{
    public static int Part1(string[] lines)
    {
        int[] head = {0,0};
        int[] tail = {0,0};
        HashSet<string> positions = new HashSet<string> { "0,0" };
        var offsets = new Dictionary<string, int[]> {
            {"R", new int[] {1,0}},
            {"L", new int[] {-1,0}},
            {"U", new int[] {0,1}},
            {"D", new int[] {0,-1}}
        };
        foreach (string l in lines) {
            string[] move = l.Split(" ");
            int[] offset = offsets[move[0]];
            for (int i=0; i<int.Parse(move[1]); i++) {
                head[0] += offset[0];
                head[1] += offset[1];
                if (Math.Abs(head[0] - tail[0]) == 2) {
                    tail[0] += (head[0] - tail[0])/2;
                    tail[1] = head[1];
                    positions.Add(tail[0].ToString()+","+tail[1].ToString());
                }
                else if (Math.Abs(head[1] - tail[1]) == 2) {
                    tail[1] += (head[1] - tail[1])/2;
                    tail[0] = head[0];
                    positions.Add(tail[0].ToString()+","+tail[1].ToString());
                }
            }
        }
        return positions.Count;
    }

    public static int Part2(string[] lines)
    {
        int[][] knots = new int[10][];
        for (int k=0; k<10; k++) knots[k] = new int[] {0,0};
        HashSet<string> positions = new HashSet<string> { "0,0" };
        var offsets = new Dictionary<string, int[]> {
            {"R", new int[] {1,0}},
            {"L", new int[] {-1,0}},
            {"U", new int[] {0,1}},
            {"D", new int[] {0,-1}}
        };
        foreach (string l in lines) {
            string[] move = l.Split(" ");
            int[] offset = offsets[move[0]];
            for (int i=0; i<int.Parse(move[1]); i++) {
                knots[0][0] += offset[0];
                knots[0][1] += offset[1];
                for (int k=1; k<10; k++) {
                    int[] head = knots[k-1];
                    int[] tail = knots[k];
                    int xDiff = head[0] - tail[0];
                    int yDiff = head[1] - tail[1];
                    if (Math.Abs(xDiff) == 2 && Math.Abs(yDiff) == 2) {
                        tail[0] += (xDiff)/2;
                        tail[1] += (yDiff)/2;
                    }
                    else if (Math.Abs(xDiff) == 2) {
                        tail[0] += (xDiff)/2;
                        tail[1] = head[1];     
                    }
                    else if (Math.Abs(yDiff) == 2) {
                        tail[1] += (yDiff)/2;
                        tail[0] = head[0];
                    }
                }
                positions.Add(knots[9][0].ToString()+","+knots[9][1].ToString());   
            }
        }
        return positions.Count;
    }
}