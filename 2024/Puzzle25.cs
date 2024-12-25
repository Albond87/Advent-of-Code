public class Puzzle25 : Puzzle
{
    public Puzzle25() : base("25") { }

    public override void Part1()
    {
        List<int[]> locks = [];
        List<int[]> keys = [];
        for (int i=0; i<inputs.Length; i+=8) {
            var rows = inputs[i..(i+7)];
            int[] heights = [0,0,0,0,0];
            for (int j=0; j<5; j++) {
                heights[j] = rows.Select(r => r[j]).Where(c => c == '#').Count()-1;
            }
            if (rows[0] == "#####") {
                locks.Add(heights);
            }
            else {
                keys.Add(heights);
            }
        }
        int[] columns = [0,1,2,3,4];
        int pairs = 0;
        foreach (var lockx in locks) {
            foreach (var key in keys) {
                if (columns.All(r => lockx[r] + key[r] <= 5)) {
                    pairs++;
                }
            }
        }
        Console.WriteLine(pairs);
    }

    public override void Part2()
    {
        Console.WriteLine("FINISHED! Happy Xmas :)");
    }
}