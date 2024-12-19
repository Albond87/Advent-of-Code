using System.Text.RegularExpressions;

public class Puzzle19 : Puzzle
{
    readonly string[] patterns;
    readonly string[] designs;

    public Puzzle19() : base("19") {
        patterns = inputs[0].Split(", ").ToArray();
        designs = inputs[2..];
    }

    // Return the number of possible designs for the given indexes of patterns and design length
    long GetPossibleDesigns(Dictionary<int,List<int>> patinds, int index, int length, Dictionary<int,long> cache) {
        if (index == length) return 1;
        if (index > length) return 0;
        if (cache.TryGetValue(index, out long result)) return result;
        long possible = 0;
        if (patinds.TryGetValue(index, out List<int>? lengths)) {
            foreach (int l in lengths) {
                long possiblel = GetPossibleDesigns(patinds, index+l, length, cache);
                possible += possiblel;
            }
        }
        cache.Add(index, possible);
        return possible;
    }

    public override void Part1()
    {
        int possible = 0;
        foreach (var d in designs) {
            Dictionary<int,List<int>> patterninds = [];
            foreach (var p in patterns) {
                Regex re = new(p);
                foreach (Match m in re.Matches(d)) {
                    if (patterninds.TryGetValue(m.Index, out List<int>? lengths)) {
                        lengths.Add(p.Length);
                    }
                    else {
                        patterninds.Add(m.Index,[p.Length]);
                    }
                }
            }
            if (GetPossibleDesigns(patterninds, 0, d.Length, []) > 0) {
                possible++;
            }
        }
        Console.WriteLine(possible);
    }

    public override void Part2()
    {
        long possible = 0;
        foreach (var d in designs) {
            Dictionary<int,List<int>> patterninds = [];
            foreach (var p in patterns) {
                // Find all instances of the pattern in the design
                // Build up a dictionary of indexes in the design and lengths of possible patterns at that index
                Regex re = new("(?=("+p+"))");
                foreach (Match m in re.Matches(d)) {
                    if (patterninds.TryGetValue(m.Index, out List<int>? lengths)) {
                        lengths.Add(p.Length);
                    }
                    else {
                        patterninds.Add(m.Index,[p.Length]);
                    }
                }
            }
            possible += GetPossibleDesigns(patterninds, 0, d.Length, []);
        }
        Console.WriteLine(possible);
    }
}