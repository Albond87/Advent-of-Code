public class Puzzle10 : Puzzle
{
    readonly List<bool[]> lights;
    readonly List<int[][]> buttons;
    readonly List<int[]> joltages;

    public Puzzle10() : base("10")
    {
        lights = [];
        buttons = [];
        joltages = [];
        foreach (var line in inputs)
        {
            int i1 = line.IndexOf(']');
            int i2 = line.IndexOf('{');
            lights.Add(line[1..i1].Select(l=>l=='#').ToArray());
            buttons.Add(line[(i1+2)..(i2-1)].Split(' ').Select(b=>b[1..^1].Split(',').Select(int.Parse).ToArray()).ToArray());
            joltages.Add(line[(i2+1)..^1].Split(',').Select(int.Parse).ToArray());
        }
    }

    static int GetMinimumPresses1(bool[] lights, int[][] buttons)
    {
        Dictionary<int[], bool[]> combos = [];
        combos[[]] = new bool[lights.Length];
        int bCount = buttons.Length;
        for (int p=1; p<bCount; p++)
        {
            Dictionary<int[], bool[]> nextCombos = [];
            foreach (var combo in combos.Keys)
            {
                int next = combo.Length == 0 ? 0 : combo[^1] + 1;
                for (int i=next; i<bCount; i++)
                {
                    bool[] nextLights = combos[combo][0..];
                    foreach (int b in buttons[i]) nextLights[b] = !nextLights[b];
                    if (nextLights.SequenceEqual(lights)) return p;
                    int[] newCombo = new int[combo.Length+1];
                    newCombo[^1] = i;
                    nextCombos[newCombo] = nextLights;
                }
            }
            combos = nextCombos;
        }
        return bCount;
    }

    static int GetMinimumPresses2(int[] joltages, int[][] buttons)
    {
        HashSet<int[]> combos = [];
        combos.Add(new int[joltages.Length]);
        int maxPresses = joltages.Sum();
        Random shuffler = new();
        for (int p=1; p<maxPresses; p++)
        {
            shuffler.Shuffle(buttons);
            HashSet<int[]> nextCombos = [];
            foreach (var combo in combos)
            {
                for (int i=0; i<buttons.Length; i++)
                {
                    int[] nextJoltages = combo[0..];
                    foreach (int b in buttons[i]) nextJoltages[b] = nextJoltages[b]+1;
                    bool success = true;
                    bool deadend = false;
                    for (int j=0; j<nextJoltages.Length; j++)
                    {
                        if (nextJoltages[j]<joltages[j]) success = false;
                        else if (nextJoltages[j]>joltages[j])
                        {
                            deadend = true;
                            break;
                        }
                    }
                    if (deadend) continue;
                    if (success) return p;
                    nextCombos.Add(nextJoltages);
                }
            }
            combos = nextCombos;
        }
        return maxPresses;
    }

    public override void Part1()
    {
        int total = 0;
        for (int i=0; i<lights.Count; i++)
        {
            total += GetMinimumPresses1(lights[i], buttons[i]);
        }
        Console.WriteLine(total);
    }

    public override void Part2()
    {
        // Attempt below - works but only for very small inputs
        // int total = 0;
        // for (int i=0; i<joltages.Count; i++)
        // {
        //     // Easy solve - a joltage is affected by every button so the total presses equals that joltage
        //     IEnumerable<int> intersection = buttons[i][0];
        //     foreach (var b in buttons[i][1..]) intersection = intersection.Intersect(b);

        //     if (intersection.Count() > 0)
        //     {
        //         total += joltages[i][intersection.First()];
        //         continue;
        //     }

        //     // Easy solve - a joltage is only affected by one button so the presses for that button equals that joltage
        //     List<List<int>> buttonsAffectingJoltages = [];
        //     for (int j=0; j<joltages[i].Length; j++)
        //     {
        //         List<int> baj = [];
        //         for (int b=0; b<buttons[i].Length; b++)
        //         {
        //             if (buttons[i][b].Contains(j)) baj.Add(b);
        //         }
        //         if (baj.Count == 1)
        //         {
        //             Console.WriteLine(inputs[i]);
        //             Console.WriteLine(baj[0] + " = " + joltages[i][j]);
        //         }
        //     }

        //     int min = GetMinimumPresses2(joltages[i], buttons[i]);
        //     total += min;
        // }
        // Console.WriteLine(total);

        Console.WriteLine("Part 2 solved in python (Puzzle10.py)");
    }
}