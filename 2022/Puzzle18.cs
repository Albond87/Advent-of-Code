public static class Puzzle18
{
    public static int Part1(string[] lines)
    {
        HashSet<Tuple<int,int,int>> cubes = new HashSet<Tuple<int,int,int>>();
        foreach (string line in lines) {
            int[] coords = line.Split(",").Select(c=>int.Parse(c)).ToArray();
            cubes.Add(new Tuple<int, int, int>(coords[0],coords[1],coords[2]));
        }
        int surfaceArea = 0;
        foreach (Tuple<int,int,int> c in cubes) {
            for (int a=0; a<3; a++) {
                for (int d=-1; d<2; d+=2) {
                    Tuple<int,int,int> c1;
                    switch (a) {
                        case 0:
                            c1 = new Tuple<int, int, int>(c.Item1+d,c.Item2,c.Item3);
                            break;
                        case 1:
                            c1 = new Tuple<int, int, int>(c.Item1,c.Item2+d,c.Item3);
                            break;
                        case 2:
                            c1 = new Tuple<int, int, int>(c.Item1,c.Item2,c.Item3+d);
                            break;    
                        default:
                            c1 = new Tuple<int, int, int>(0,0,0);
                            break;                    
                    }
                    if (!cubes.Contains(c1)) surfaceArea++;
                }
            }
        }
        return surfaceArea;
    }

    public static long Part2(string[] lines)
    {
        HashSet<Tuple<int,int,int>> cubes = new HashSet<Tuple<int,int,int>>();
        int[][] bounds = new int[][]{new int[]{1000,-1000},new int[]{1000,-1000},new int[]{1000,-1000}};
        foreach (string line in lines) {
            int[] coords = line.Split(",").Select(c=>int.Parse(c)).ToArray();
            cubes.Add(new Tuple<int, int, int>(coords[0],coords[1],coords[2]));
            for (int i=0; i<3; i++) {
                if (coords[i] < bounds[i][0]) bounds[i][0] = coords[i];
                if (coords[i] > bounds[i][1]) bounds[i][1] = coords[i];
            }
        }
        int surfaceArea = 0;
        HashSet<Tuple<int,int,int>> outerGaps = new HashSet<Tuple<int,int,int>>();
        HashSet<Tuple<int,int,int>> innerGaps = new HashSet<Tuple<int,int,int>>();
        foreach (Tuple<int,int,int> c in cubes) {
            for (int a=0; a<3; a++) {
                for (int d=-1; d<2; d+=2) {
                    Tuple<int,int,int> c1;
                    switch (a) {
                        case 0:
                            c1 = new Tuple<int, int, int>(c.Item1+d,c.Item2,c.Item3);
                            break;
                        case 1:
                            c1 = new Tuple<int, int, int>(c.Item1,c.Item2+d,c.Item3);
                            break;
                        case 2:
                            c1 = new Tuple<int, int, int>(c.Item1,c.Item2,c.Item3+d);
                            break;    
                        default:
                            c1 = new Tuple<int, int, int>(0,0,0);
                            break;                    
                    }
                    if (!cubes.Contains(c1)) {
                        if (outerGaps.Contains(c1)) {
                            surfaceArea++;
                        }
                        else if (innerGaps.Contains(c1)) {
                            continue;
                        }
                        else {
                            HashSet<Tuple<int,int,int>> gaps = new HashSet<Tuple<int,int,int>>();
                            gaps.Add(c1);
                            int result=0;
                            while (result==0) {
                                HashSet<Tuple<int,int,int>> newGaps = new HashSet<Tuple<int,int,int>>();
                                foreach (Tuple<int,int,int> g in gaps) {                                    
                                    if (outerGaps.Contains(g) || bounds[0].Contains(g.Item1) || bounds[1].Contains(g.Item2) || bounds[2].Contains(g.Item3)) {
                                        surfaceArea++;
                                        foreach (Tuple<int,int,int> e in gaps) {
                                            outerGaps.Add(e);
                                        }
                                        foreach (Tuple<int,int,int> e in newGaps) {
                                            outerGaps.Add(e);
                                        }
                                        result=1;
                                        break;
                                    }
                                    else {
                                        result=2;
                                        for (int ag=0; ag<3; ag++) {
                                            for (int dg=-1; dg<2; dg+=2) {
                                                Tuple<int,int,int> g1;
                                                switch (ag) {
                                                    case 0:
                                                        g1 = new Tuple<int, int, int>(g.Item1+dg,g.Item2,g.Item3);
                                                        break;
                                                    case 1:
                                                        g1 = new Tuple<int, int, int>(g.Item1,g.Item2+dg,g.Item3);
                                                        break;
                                                    case 2:
                                                        g1 = new Tuple<int, int, int>(g.Item1,g.Item2,g.Item3+dg);
                                                        break;    
                                                    default:
                                                        g1 = new Tuple<int, int, int>(0,0,0);
                                                        break;                    
                                                }
                                                if (!cubes.Contains(g1) && !gaps.Contains(g1)) {
                                                    newGaps.Add(g1);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (result != 1 && newGaps.Count() > 0) {
                                    result=0;
                                    foreach (Tuple<int,int,int> e in newGaps) {
                                        gaps.Add(e);
                                    }
                                }
                            }
                            if (result == 2) {
                                foreach (Tuple<int,int,int> g in gaps) {
                                    innerGaps.Add(g);
                                }
                            }
                        }
                    }
                }
            }
        }
        return surfaceArea;
    }
}