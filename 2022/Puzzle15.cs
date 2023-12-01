public static class Puzzle15
{
    public static int Part1(string[] lines)
    {
        int rowToCount = 2000000;
        HashSet<int> beaconsInRow = new HashSet<int>();
        HashSet<int> nonBeaconsInRow = new HashSet<int>();
        foreach (string l in lines) {
            int[] sensor = l.Split(new string[]{"Sensor at x=",", y=",": closest beacon is at x="},StringSplitOptions.RemoveEmptyEntries).Select(s=>int.Parse(s)).ToArray();
            int dist = Math.Abs(sensor[2]-sensor[0]) + Math.Abs(sensor[3]-sensor[1]);
            int fromRow = Math.Abs(rowToCount-sensor[1]);
            if (fromRow<=dist) {
                if (sensor[3] == rowToCount) beaconsInRow.Add(sensor[2]);
                for (int x=sensor[0]-(dist-fromRow); x<=sensor[0]+(dist-fromRow); x++) {
                    if (!beaconsInRow.Contains(x))
                        nonBeaconsInRow.Add(x);
                }
            }
        }
        return nonBeaconsInRow.Count();
    }

    public static long Part2(string[] lines)
    {
        long spaceSize = 4000000;
        List<int[]> sensors = lines.Select(l=>l.Split(new string[]{"Sensor at x=",", y=",": closest beacon is at x="},StringSplitOptions.RemoveEmptyEntries).Select(s=>int.Parse(s)).ToArray()).ToList();
        foreach (int[] s in sensors) {
            int dist = Math.Abs(s[2]-s[0]) + Math.Abs(s[3]-s[1])+1;
            int[] pos = new int[]{s[0]-dist,s[1]};
            for (int xd=1; xd>-2; xd-=2) {
                for (int yd=xd; yd<=1 && yd>=-1; yd+=xd*-2) {
                    for (int i=0; i<dist; i++) {
                        if (!(pos[0]<0 || pos[0]>spaceSize || pos[1]<0 || pos[1]>spaceSize)) {
                            bool possible=true;
                            foreach (int[] s2 in sensors) {
                                int dist2 = Math.Abs(s2[2]-s2[0]) + Math.Abs(s2[3]-s2[1]);
                                int dist3 = Math.Abs(pos[0]-s2[0]) + Math.Abs(pos[1]-s2[1]);
                                if (dist3 <= dist2) {
                                    possible=false;
                                    break;
                                }
                            }
                            if (possible) return (long)pos[0]*4000000+(long)pos[1];
                        }                            
                        pos[0]+=xd;
                        pos[1]+=yd;
                    }         
                }
            }
        }
        return -1; // no possible beacons
    }
}