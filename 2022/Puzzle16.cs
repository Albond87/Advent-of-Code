public static class Puzzle16
{
    class Valve {
        public Valve(int f, string[] t) {
            this.flowRate = f;
            this.tunnels = t;
            this.open = false;
        }
        public int flowRate;
        public string[] tunnels;
        public bool open;
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

    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> sequence)
    {
        if (sequence == null)
        {
            yield break;
        }

        var list = sequence.ToList();

        if (!list.Any())
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            var startingElementIndex = 0;

            foreach (var startingElement in list)
            {
                var index = startingElementIndex;
                var remainingItems = list.Where((e, i) => i != index);
                if (remainingItems.Count() < 13) yield return new List<T>();
                foreach (var permutationOfRemainder in remainingItems.Permute())
                {
                    yield return permutationOfRemainder.Prepend(startingElement);
                }

                startingElementIndex++;
            }
        }
    }

    static IEnumerable<IList<T>> GetVariations<T>(IList<T> offers, int length)
    {
        var startIndices = new int[length];
        for (int i = 0; i < length; ++i)
            startIndices[i] = i;

        var indices = new HashSet<int>(); // for duplicate check

        while (startIndices[0] < offers.Count)
        {
            var variation = new List<T>(length);
            for (int i = 0; i < length; ++i)
            {
                variation.Add(offers[startIndices[i]]);
            }
            yield return variation;

            //Count up the indices
            AddOne(startIndices, length - 1, offers.Count - 1);

            //duplicate check                
            var check = true;
            while (check)
            {
                indices.Clear();                    
                for (int i = 0; i <= length; ++i)
                {
                    if (i == length)
                    {
                        check = false;
                        break;
                    }
                    if (indices.Contains(startIndices[i]))
                    {
                        var unchangedUpTo = AddOne(startIndices, i, offers.Count - 1);
                        indices.Clear();
                        for (int j = 0; j <= unchangedUpTo; ++j )
                        {
                            indices.Add(startIndices[j]);
                        }
                        int nextIndex = 0;
                        for(int j = unchangedUpTo + 1; j < length; ++j)
                        {
                            while (indices.Contains(nextIndex))
                                nextIndex++;
                            startIndices[j] = nextIndex++;
                        }
                        break;
                    }

                    indices.Add(startIndices[i]);
                }
            }
        }
    }

    static int AddOne(int[] indices, int position, int maxElement)
    {
        //returns the index of the last element that has not been changed

        indices[position]++;
        for (int i = position; i > 0; --i)
        {
            if (indices[i] > maxElement)
            {
                indices[i] = 0;
                indices[i - 1]++;
            }
            else
                return i;
        }
        return 0;
    }

    public static int Part1(string[] lines)
    {
        //Valve current = new Valve(0,new string[]{});
        Dictionary<string,Valve> valves = new Dictionary<string, Valve>();
        List<string> nonZero = new List<string>();
        foreach (string l in lines) {
            string[] parts = l.Split(new string[] {"Valve "," has flow rate=","; tunnel leads to valve ","; tunnels lead to valves "},StringSplitOptions.RemoveEmptyEntries);
            //foreach (string p in parts) Console.WriteLine(p);
            string[] tunnels = parts[2].Split(", ");
            valves[parts[0]] = new Valve(int.Parse(parts[1]),tunnels);
            //if (parts[0] == "AA") current = valves[parts[0]];
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
        foreach (KeyValuePair<string, Dictionary<string, int>> n1 in interDistances) {
            Console.Write(n1.Key + " | ");
            foreach (KeyValuePair<string, int> n2 in n1.Value) {
                Console.Write(n2.Key + ": " + n2.Value + ", ");
            }
            Console.WriteLine();
        }
        
        IEnumerable<IEnumerable<string>> routes = GetVariations(nonZero,7);
        Console.WriteLine(routes.Count());
        // foreach (IEnumerable<string> route in routes) {
        //     foreach (string n in route) Console.Write(n + " ");
        //     Console.WriteLine();
        // }
        int maxPressure = 0;

        foreach (IEnumerable<string> r in routes) {
            string[] route = r.ToArray();
            int time = interDistances["AA"][route[0]]+1;
            int current=0;
            int pressure=0;
            while (time<30) {
                pressure += (30-time) * valves[route[current]].flowRate;
                current++;
                if (current<route.Length) {
                    time += interDistances[route[current-1]][route[current]]+1;
                } else break;
            }
            if (pressure>maxPressure) maxPressure=pressure;
        }
        // for (int i=29; i>=0; i--) {
        //     if (!current.open && current.flowRate > 0) {
        //         current.open = true;
        //         pressure += i*current.flowRate;
        //     } else {
        //         string next = "";
        //         foreach (string t in current.tunnels) {
        //             if (!valves[t].open && valves[t].flowRate > 0) {
        //                 next = t;
        //                 break;
        //             }
        //         }
        //         if (next != "") {
        //             Console.WriteLine(next);
        //             current = valves[next];
        //         }
        //         else {
        //             Console.WriteLine(current.tunnels[0]);
        //             current = valves[current.tunnels[0]];
        //         }
        //     }
        // }
        return maxPressure;
    }

    public static int Part2(string[] lines)
    {
        return 0;
    }
}