public static class Puzzle14
{
    public static int Part1(string[] lines)
    {
        Dictionary<int,HashSet<int>> points = new Dictionary<int, HashSet<int>>();
        int lowest = 0;
        foreach (string l in lines) {
            List<string[]> coords = l.Split(" -> ").Select(c => c.Split(",")).ToList();
            int[] start = new int[] {int.Parse(coords[0][0]), int.Parse(coords[0][1])};
            HashSet<int> row = points.GetValueOrDefault(start[1],new HashSet<int>());
            row.Add(start[0]);
            points[start[1]] = row;
            if (start[1] > lowest) lowest = start[1];
            foreach (string[] c in coords.Skip(1)) {
                int[] end = new int[] {int.Parse(c[0]), int.Parse(c[1])};
                if (start[1]==end[1]) { // y positions the same
                    row = points.GetValueOrDefault(start[1],new HashSet<int>());
                    if (start[0] < end[0]) {
                        for (int p=start[0]+1; p<=end[0]; p++)
                            row.Add(p);
                    } else { 
                        for (int p=start[0]-1; p>=end[0]; p--)
                            row.Add(p);
                    }
                    points[start[1]] = row;
                } else { // x positions the same
                    List<int> ys = new List<int>();
                    if (start[1] < end[1]) {
                        for (int p=start[1]+1; p<=end[1]; p++)
                            ys.Add(p);
                    } else {
                        for (int p=start[1]-1; p>=end[1]; p--)
                            ys.Add(p);
                    }
                    ys.ForEach(y => {
                        HashSet<int> row = points.GetValueOrDefault(y,new HashSet<int>());
                        row.Add(start[0]);
                        points[y] = row;
                        if (y>lowest) lowest=y;
                    });
                }
                start = end;
            }
        } 
        int sandCount = 0;
        while (true) {
            int[] sand = new int[]{500,0};
            bool resting = false;
            HashSet<int>? row;
            while (!resting) {
                if (sand[1] == lowest) return sandCount;
                if (points.TryGetValue(sand[1]+1,out row)) {
                    if (row.Contains(sand[0])) {
                        if (!row.Contains(sand[0]-1)) { sand[1]++; sand[0]--; continue; }
                        else if (!row.Contains(sand[0]+1)) { sand[1]++; sand[0]++; continue; }
                    } else { sand[1]++; continue; }
                } 
                else { sand[1]++; continue; }
                resting = true;
            }
            row = points.GetValueOrDefault(sand[1], new HashSet<int>());
            row.Add(sand[0]);
            points[sand[1]] = row;
            sandCount++;
        }
    }

    public static int Part2(string[] lines)
    {
        Dictionary<int,HashSet<int>> points = new Dictionary<int, HashSet<int>>();
        int lowest = 0;
        foreach (string l in lines) {
            List<string[]> coords = l.Split(" -> ").Select(c => c.Split(",")).ToList();
            int[] start = new int[] {int.Parse(coords[0][0]), int.Parse(coords[0][1])};
            HashSet<int> row = points.GetValueOrDefault(start[1],new HashSet<int>());
            row.Add(start[0]);
            points[start[1]] = row;
            if (start[1] > lowest) lowest = start[1];
            foreach (string[] c in coords.Skip(1)) {
                int[] end = new int[] {int.Parse(c[0]), int.Parse(c[1])};
                if (start[1]==end[1]) { // y positions the same
                    row = points.GetValueOrDefault(start[1],new HashSet<int>());
                    if (start[0] < end[0]) {
                        for (int p=start[0]+1; p<=end[0]; p++)
                            row.Add(p);
                    } else { 
                        for (int p=start[0]-1; p>=end[0]; p--)
                            row.Add(p);
                    }
                    points[start[1]] = row;
                } else { // x positions the same
                    List<int> ys = new List<int>();
                    if (start[1] < end[1]) {
                        for (int p=start[1]+1; p<=end[1]; p++)
                            ys.Add(p);
                    } else {
                        for (int p=start[1]-1; p>=end[1]; p--)
                            ys.Add(p);
                    }
                    ys.ForEach(y => {
                        HashSet<int> row = points.GetValueOrDefault(y,new HashSet<int>());
                        row.Add(start[0]);
                        points[y] = row;
                        if (y>lowest) lowest=y;
                    });
                }
                start = end;
            }
        } 
        int sandCount = 0;
        while (true) {
            int[] sand = new int[]{500,0};
            bool resting = false;
            HashSet<int>? row;
            while (!resting) {
                if (sand[1] == lowest+1) { resting=true; continue; };
                if (points.TryGetValue(sand[1]+1,out row)) {
                    if (row.Contains(sand[0])) {
                        if (!row.Contains(sand[0]-1)) { sand[1]++; sand[0]--; continue; }
                        else if (!row.Contains(sand[0]+1)) { sand[1]++; sand[0]++; continue; }
                    } else { sand[1]++; continue; }
                } 
                else { sand[1]++; continue; }
                resting = true;
            }
            if (sand[1] == 0) return sandCount+1;
            row = points.GetValueOrDefault(sand[1], new HashSet<int>());
            row.Add(sand[0]);
            points[sand[1]] = row;
            sandCount++;
        }
    }
}