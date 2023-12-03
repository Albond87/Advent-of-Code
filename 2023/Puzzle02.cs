using System.Text.RegularExpressions;

public class Puzzle02 : Puzzle
{
    public Puzzle02() : base("02") { }

    static bool IsPossible(string game, Dictionary<string, int> limits) {
        foreach (var c in limits) {                
            foreach (Match match in Regex.Matches(game, @"[0-9]+ " + c.Key).Cast<Match>()) {
                int count = int.Parse(match.Value.Split(" ")[0]);
                if (count > c.Value) {
                    return false;
                }
            }
        }
        return true;
    }

    public override void Part1()
    {
        // ==================== Refined solution using Regex =====================
        Dictionary<string,int> limits = new Dictionary<string, int> { {"red", 12}, {"green", 13}, {"blue", 14} };
        int sum = 0;

        for (int i=0; i<inputs.Length; i++) {
            if (IsPossible(inputs[i], limits)) {
                sum += i+1;
            }
        }
        Console.WriteLine(sum);
        
        // ========================== Original solution ==========================
        // Dictionary<string,int> limits = new Dictionary<string, int> { {"red", 12}, {"green", 13}, {"blue", 14} };
        // int sum = 0;

        // for (int i=0; i<inputs.Length; i++) {
        //     string[] sets = inputs[i].Split(": ")[1].Split("; ");
        //     bool possible = true;
        //     foreach (string set in sets)
        //     {
        //         foreach (string colour in set.Split(", ")) {
        //             string[] parts = colour.Split(" ");
        //             if (int.Parse(parts[0]) > limits[parts[1]]) {
        //                 possible = false;
        //             }
        //         }
        //     }
        //     if (possible) {
        //         sum += i+1;
        //     }
        // }
        // Console.WriteLine(sum);        
    }

    public override void Part2()
    {
        // ==================== Refined solution using Regex =====================
        string[] colours = ["red", "green", "blue"];
        int sum = 0;

        for (int i=0; i<inputs.Length; i++) {
            int power = 1;
            foreach (string c in colours) {                
                power *= Regex.Matches(inputs[i], @"[0-9]+ " + c).Cast<Match>().Select(m => int.Parse(m.Value.Split(" ")[0])).Max();
            }
            sum += power;
        }
        Console.WriteLine(sum);

        // ========================== Original solution ==========================
        // int sum = 0;

        // for (int i=0; i<inputs.Length; i++) {
        //     Dictionary<string,int> maximums = new Dictionary<string, int> { {"red", 0}, {"green", 0}, {"blue", 0} };
        //     string[] sets = inputs[i].Split(": ")[1].Split("; ");
        //     foreach (string set in sets)
        //     {
        //         foreach (string colour in set.Split(", ")) {
        //             string[] parts = colour.Split(" ");
        //             if (int.Parse(parts[0]) > maximums[parts[1]]) {
        //                 maximums[parts[1]] = int.Parse(parts[0]);
        //             }
        //         }
        //     }
        //     sum += maximums["red"] * maximums["green"] * maximums["blue"];
        // }
        // Console.WriteLine(sum);
    }
}