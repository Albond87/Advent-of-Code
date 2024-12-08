public class Puzzle08 : Puzzle
{
    public Puzzle08() : base("08") { }

    int CountAntinodes(bool includeHarmonics=false) {
        HashSet<char> frequencies = [];
        HashSet<(int,int)> antinodes = [];
        int height = inputs.Length;
        int width = inputs[0].Length;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                char antenna = inputs[y][x];
                if (antenna != '.' && !frequencies.Contains(antenna)) {
                    for (int y2 = y; y2 < height; y2++) {
                        int startx = y2 == y ? x + 1 : 0;
                        for (int x2 = startx; x2 < width; x2++) {
                            if (inputs[y2][x2] == antenna) {
                                int xdiff = x2 - x;
                                int ydiff = y2 - y;
                                int x3, y3;
                                if (!includeHarmonics) {
                                    // Part 1
                                    x3 = x2 + xdiff;
                                    y3 = y2 + ydiff;
                                    if (x3 >= 0 && x3 < width && y3 < height) {
                                        antinodes.Add((x3,y3));
                                    }
                                    x3 = x - xdiff;
                                    y3 = y - ydiff;
                                    if (x3 >= 0 && x3 < width && y3 >= 0) {
                                        antinodes.Add((x3,y3));
                                    }
                                    continue;
                                }
                                // Part 2
                                x3 = x2;
                                y3 = y2;
                                while (x3 >= 0 && x3 < width && y3 < height) {
                                    antinodes.Add((x3,y3));
                                    x3 += xdiff;
                                    y3 += ydiff;
                                }
                                x3 = x;
                                y3 = y;
                                while (x3 >= 0 && x3 < width && y3 >= 0) {
                                    antinodes.Add((x3,y3));
                                    x3 -= xdiff;
                                    y3 -= ydiff;
                                }
                            }
                        }
                    }
                }
            }
        }
        return antinodes.Count;
    }

    public override void Part1()
    {
        Console.WriteLine(CountAntinodes());
    }

    public override void Part2()
    {
        Console.WriteLine(CountAntinodes(true));
    }
}