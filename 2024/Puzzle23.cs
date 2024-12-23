public class Puzzle23 : Puzzle
{
    HashSet<(string,string)> connections = [];

    public Puzzle23() : base("23") { 
        foreach (var input in inputs) {
            connections.Add((input.Split("-")[0],input.Split("-")[1]));
            connections.Add((input.Split("-")[1],input.Split("-")[0]));
        }
    }

    public override void Part1()
    {
        HashSet<(string,string,string)> countedsets = [];
        int count = 0;
        foreach (var connection in connections) {
            if (connection.Item1.StartsWith('t')) {
                var setsof3 = connections.Where(c => c.Item1 == connection.Item2 && connections.Contains((c.Item2, connection.Item1)));
                foreach (var set in setsof3) {
                    string[] computers = new string[]{connection.Item1, set.Item1, set.Item2}.Order().ToArray();
                    (string,string,string) computerstuple = (computers[0],computers[1],computers[2]);
                    if (!countedsets.Contains(computerstuple)) {
                        count++;
                        countedsets.Add(computerstuple);
                    }
                }
            }
        }
        Console.WriteLine(count);
    }

    public override void Part2()
    {
        // BFS the graph for sets that are fully connected
        List<HashSet<string>> connectedsets = [];
        HashSet<string> explored = [];
        foreach (var connection in connections) {
            if (explored.Contains(connection.Item1)) {
                continue;
            }
            HashSet<string> currentset = [];
            Queue<string> frontier = [];
            frontier.Enqueue(connection.Item1);
            while (frontier.Count > 0) {
                string computer = frontier.Dequeue();

                // Check that the computer is fully connected to everything else in the current set
                bool allconnected = true;
                foreach (var s in currentset) {
                    if (!connections.Contains((computer,s))) {
                        allconnected = false;
                        break;
                    }
                }
                if (!allconnected) {
                    continue;
                }

                // Find everything that's connected to the current computer and add them to the frontier if not already explored
                var connected = connections.Where(c => c.Item1 == computer);
                foreach (var c in connected) {
                    if (!currentset.Contains(c.Item2)) {
                        frontier.Enqueue(c.Item2);
                    }
                }
                currentset.Add(computer);
                explored.Add(computer);
            }
            connectedsets.Add(currentset);
        }
        // Find the largest fully connected set
        var largestset = connectedsets.MaxBy(s => s.Count);
        if (largestset != null) {
            Console.WriteLine(string.Join(',',largestset.ToList().Order()));
        }
    }
}