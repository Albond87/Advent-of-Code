public static class Puzzle16
{
    class Valve {
        public Valve(int f, string[] t) {
            this.flowRate = f;
            this.tunnels = t;
        }
        public int flowRate;
        public string[] tunnels;
    }

    static int distanceBetweenNodes(string start, string goal, ref Dictionary<string,Valve> valves) {
        if (start == goal) return 0;
        int dist = 0;
        List<string> visited = new List<string>();
        List<string> frontier = new List<string>();
        frontier.Add(start);
        while (frontier.Count() > 0) {
            dist++;
            List<string> next = new List<string>();
            foreach (string node in frontier) {
                foreach (string t in valves[node].tunnels) {
                    if (t == goal) return dist;
                    if (!frontier.Contains(t) && !visited.Contains(t) && !next.Contains(t)) next.Add(t);
                }
                visited.Add(node);
            }
            frontier = next;
        }
        return -1; 
    }

    static int MaxPressure(ref Dictionary<string,Valve> valves, List<string> toOpen, string current, ref Dictionary<string, Dictionary<string, int>> distances, int time, int pressure) {
        if (time>28 || toOpen.Count()==0) return pressure;
        int max = 0;
        foreach (string v in toOpen) {
            int newTime = time+distances[current][v]+1;
            if (newTime > 29) continue;
            int newPressure = pressure+((30-newTime)*valves[v].flowRate);
            int p = MaxPressure(ref valves, toOpen.Where(o=>o!=v).ToList(), v, ref distances, newTime, pressure+((30-newTime)*valves[v].flowRate));
            if (p > max) max = p;
        }
        return max==0?pressure:max;
    }

    static int MaxPressureWithElephant(ref Dictionary<string,Valve> valves, List<string> toOpen, string current, string elephant, int steps, int eleSteps, ref Dictionary<string, Dictionary<string, int>> distances, int time, int pressure) {
        if (time >= 23 || toOpen.Count() == 0) return pressure;
        if (steps == 0) {
            int max = 0;
            if (eleSteps == 0) {
                foreach (string a in toOpen) {
                    int aTime = distances[current][a]+1;
                    int newTime = time+aTime;
                    if (newTime>25) continue;
                    int aPressure = pressure+((26-newTime)*valves[a].flowRate);
                    foreach (string b in toOpen) {
                        //string b = toOpen.Skip(1).First();
                        if (a==b) continue;
                        int bTime = distances[elephant][b]+1;
                        newTime = time+bTime;
                        if (newTime>25) continue;
                        int newPressure = aPressure+((26-newTime)*valves[b].flowRate);
                        int p = MaxPressureWithElephant(ref valves, toOpen.Where(o=>o!=a&&o!=b).ToList(), a, b, aTime-1, bTime-1, ref distances, time+1, newPressure);
                        if (p>max) max=p;
                    }
                }
                return max;
            }
            foreach (string a in toOpen) {
                int aTime = distances[current][a]+1;
                int newTime = time+aTime;
                if (newTime>25) continue;
                int newPressure = pressure+((26-newTime)*valves[a].flowRate);
                int p = MaxPressureWithElephant(ref valves, toOpen.Where(o=>o!=a).ToList(), a, elephant, aTime-1, eleSteps-1, ref distances, time+1, newPressure);
                if (p>max) max=p;
            }
            return max;
        }
        else if (eleSteps == 0) {
            int max = 0;
            foreach (string b in toOpen) {
                int bTime = distances[elephant][b]+1;
                int newTime = time+bTime;
                if (newTime>25) continue;
                int newPressure = pressure+((26-newTime)*valves[b].flowRate);
                int p = MaxPressureWithElephant(ref valves, toOpen.Where(o=>o!=b).ToList(), current, b, steps-1, bTime-1, ref distances, time+1, newPressure);
                if (p>max) max=p;
            }
            return max;
        }
        else {
            return MaxPressureWithElephant(ref valves, toOpen, current, elephant, steps-1, eleSteps-1, ref distances, time+1, pressure);
        }
    }

    public static int Part1(string[] lines)
    {
        Dictionary<string,Valve> valves = new Dictionary<string, Valve>();
        List<string> nonZero = new List<string>();
        foreach (string l in lines) {
            string[] parts = l.Split(new string[] {"Valve "," has flow rate=","; tunnel leads to valve ","; tunnels lead to valves "},StringSplitOptions.RemoveEmptyEntries);
            string[] tunnels = parts[2].Split(", ");
            valves[parts[0]] = new Valve(int.Parse(parts[1]),tunnels);
            if (parts[1] != "0") nonZero.Add(parts[0]);
        }
        Dictionary<string, Dictionary<string, int>> interDistances = new Dictionary<string, Dictionary<string, int>>();
        interDistances["AA"] = new Dictionary<string, int>();
        foreach (string n in nonZero) {
            interDistances["AA"][n] = distanceBetweenNodes("AA",n,ref valves);
            Dictionary<string, int> ds = interDistances.GetValueOrDefault(n,new Dictionary<string, int>());
            foreach (string t in nonZero) {
                if (t == n) continue;
                if (!ds.ContainsKey(t)) {
                    int dist = distanceBetweenNodes(n,t,ref valves);
                    ds[t] = dist;
                    Dictionary<string, int> dt = interDistances.GetValueOrDefault(t,new Dictionary<string, int>());
                    dt[n] = dist;
                    interDistances[t] = dt;
                }
            }
            interDistances[n] = ds;
        }
        return MaxPressure(ref valves, nonZero, "AA", ref interDistances, 0, 0);
    }

    public static int Part2(string[] lines)
    {
        Dictionary<string,Valve> valves = new Dictionary<string, Valve>();
        List<string> nonZero = new List<string>();
        foreach (string l in lines) {
            string[] parts = l.Split(new string[] {"Valve "," has flow rate=","; tunnel leads to valve ","; tunnels lead to valves "},StringSplitOptions.RemoveEmptyEntries);
            string[] tunnels = parts[2].Split(", ");
            valves[parts[0]] = new Valve(int.Parse(parts[1]),tunnels);
            if (parts[1] != "0") nonZero.Add(parts[0]);
        }
        Dictionary<string, Dictionary<string, int>> interDistances = new Dictionary<string, Dictionary<string, int>>();
        interDistances["AA"] = new Dictionary<string, int>();
        foreach (string n in nonZero) {
            interDistances["AA"][n] = distanceBetweenNodes("AA",n,ref valves);
            Dictionary<string, int> ds = interDistances.GetValueOrDefault(n,new Dictionary<string, int>());
            foreach (string t in nonZero) {
                if (t == n) continue;
                if (!ds.ContainsKey(t)) {
                    int dist = distanceBetweenNodes(n,t,ref valves);
                    ds[t] = dist;
                    Dictionary<string, int> dt = interDistances.GetValueOrDefault(t,new Dictionary<string, int>());
                    dt[n] = dist;
                    interDistances[t] = dt;
                }
            }
            interDistances[n] = ds;
        }
        Dictionary<string,int> cache = new Dictionary<string, int>();
        return MaxPressureWithElephant(ref valves, nonZero, "AA", "AA", 0, 0, ref interDistances, 0, 0);
    }
}