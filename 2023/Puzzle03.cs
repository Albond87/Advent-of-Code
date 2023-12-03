using System.Text.RegularExpressions;

public class Puzzle03 : Puzzle
{
    readonly int width;
    readonly int height;
    readonly int[][][] locations;

    public Puzzle03() : base("03") { 
        width = inputs[0].Length;
        height = inputs.Length;
        locations = inputs.Select(i => Regex.Matches(i, @"[0-9]+").Cast<Match>().Select(m => new[] {m.Index, m.Length, int.Parse(m.Value)}).ToArray()).ToArray();
    }

    public override void Part1()
    {
        int sum = 0;
        for (int y=0; y<height; y++) {
            foreach (var num in locations[y]) {
                int left = num[0] == 0 ? 0 : num[0] - 1;
                int right = num[0]+num[1] == width ? num[0]+num[1] : num[0]+num[1] + 1;
                if (y>0) {
                    if (inputs[y-1].Substring(left,right-left) != new string('.',right-left)) {
                        sum += num[2];
                        continue;
                    }
                }
                if (num[0] > 0) {
                    if (inputs[y][num[0]-1] != '.') {
                        sum += num[2];
                        continue;
                    }
                }
                if (num[0]+num[1] < width) {
                    if (inputs[y][num[0]+num[1]] != '.') {
                        sum += num[2];
                        continue;
                    }
                }
                if (y<height-1) {
                    if (inputs[y+1].Substring(left,right-left) != new string('.',right-left)) {
                        sum += num[2];
                        continue;
                    }
                }
            }
        }
        Console.WriteLine(sum);
    }

    public override void Part2()
    {
        int sum = 0;
        for (int y=0; y<height; y++) {
            int x=0;
            while (x > -1)
            {
                x=inputs[y].IndexOf('*',x);
                if (x>-1) {
                    List<int> adjacent = [];
                    if (y>0) {
                        foreach (var num in locations[y-1]) {
                            if ((num[0] < x+2 && num[0] > x-2) || (num[0] + num[1] - 1 < x+2 && num[0] + num[1] - 1 > x-2)) {
                                adjacent.Add(num[2]);
                            }
                        }
                    }
                    if (x>0 && inputs[y][x-1] != '.') {
                        foreach (var num in locations[y]) {
                            if (num[0]+num[1]==x) {
                                adjacent.Add(num[2]);
                            }
                        }
                    }
                    if (x<width-1 && inputs[y][x+1] != '.') {
                        foreach (var num in locations[y]) {
                            if (num[0]==x+1) {
                                adjacent.Add(num[2]);
                            }
                        }
                    }
                    if (y<height-1) {
                        foreach (var num in locations[y+1]) {
                            if ((num[0] < x+2 && num[0] > x-2) || (num[0] + num[1] - 1 < x+2 && num[0] + num[1] - 1 > x-2)) {
                                adjacent.Add(num[2]);
                            }
                        }
                    }
                    if (adjacent.Count == 2) {
                        sum += adjacent[0] * adjacent[1];
                    }
                    x++;
                }
            }
        }
        Console.WriteLine(sum);
    }
}