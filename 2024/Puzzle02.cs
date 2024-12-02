public class Puzzle02 : Puzzle
{
    readonly List<List<int>> reports;

    public Puzzle02() : base("02") { 
        reports = inputs.Select(i => i.Split(" ").Select(n => int.Parse(n)).ToList()).ToList();
    }

    public override void Part1()
    {
        int safe = 0;
        foreach (var report in reports) {
            var deltas = report.Skip(1).Select((level, i) => level - report.ElementAt(i)).ToList();
            int increasing = deltas.Count(d => d > 0);
            if ((increasing == 0 || increasing == deltas.Count) && deltas.All(d => Math.Abs(d) > 0 && Math.Abs(d) < 4)) {
                safe++;
            }
        }
        Console.WriteLine(safe);
    }

    public override void Part2()
    {
        int safe = 0;
        foreach (var report in reports) {
            var deltas = report.Skip(1).Select((level, i) => level - report.ElementAt(i)).ToList();
            int increasing = deltas.Count(d => d > 0);
            int decreasing = deltas.Count(d => d < 0);
            int big = deltas.Count(d => Math.Abs(d) > 3);
            int small = deltas.Count(d => Math.Abs(d) < 1);
            
            if (increasing == 0 || decreasing == 0) {
                if (big == 0 && small <= 1) {
                    safe++;
                }
                else if (small == 0 && big == 1) {
                    int bigloc = deltas.FindIndex(d => Math.Abs(d) > 3);
                    if (bigloc == 0 || bigloc == deltas.Count - 1) {
                        safe++;
                    }
                }
            }
            else {
                // If there is only one step in the wrong direction, find it and try removing the element either side of it
                if ((increasing == 1 && decreasing == report.Count - 2) || (decreasing == 1 && increasing == report.Count - 2)) {
                    int error_index = 0;
                    if (increasing == 1) {
                        error_index = deltas.FindIndex(d => d > 0);
                    }
                    else {
                        error_index = deltas.FindIndex(d => d < 0);
                    }
                    for (int k = 0; k < 2; k++) {
                        var new_report = report.Take(error_index + k).Concat(report.Skip(error_index + k + 1)).ToList();
                        deltas = new_report.Skip(1).Select((level, i) => level - new_report.ElementAt(i)).ToList();
                        increasing = deltas.Count(d => d > 0);
                        if ((increasing == 0 || increasing == deltas.Count) && deltas.All(d => Math.Abs(d) > 0 && Math.Abs(d) < 4)) {
                            safe++;
                            break;
                        }
                    }
                }

                // Alternative - brute-force check validity after removing each element individually
                // for (int k = 0; k < report.Count; k++) {
                //     var new_report = report.Take(k).Concat(report.Skip(k+1));
                //     deltas = new_report.Skip(1).Select((level, i) => level - new_report.ElementAt(i)).ToList();
                //     increasing = deltas.Count(d => d > 0);
                //     if ((increasing == 0 || increasing == deltas.Count) && deltas.All(d => Math.Abs(d) > 0 && Math.Abs(d) < 4)) {
                //         safe++;
                //         break;
                //     }
                // }
            }
        }
        Console.WriteLine(safe);
    }
}