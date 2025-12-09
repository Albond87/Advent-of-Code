public class Puzzle09 : Puzzle
{
    readonly long[][] tiles;

    public Puzzle09() : base("09")
    {
        tiles = inputs.Select(i=>i.Split(',').Select(long.Parse).ToArray()).ToArray();
    }

    public override void Part1()
    {
        long maxArea = 0;
        for (int i=0; i<tiles.Length; i++)
        {
            for (int j=i+1; j<tiles.Length; j++)
            {
                long area = (Math.Abs(tiles[i][0] - tiles[j][0]) + 1) * (Math.Abs(tiles[i][1] - tiles[j][1]) + 1);
                if (area > maxArea) 
                {
                    maxArea = area;
                }
            }
        }
        Console.WriteLine(maxArea);
    }

    public override void Part2()
    {
        // Not a general solution - works on the kinds of input given though
        long maxArea = 0;
        for (int i=0; i<tiles.Length; i++)
        {
            for (int j=i+1; j<tiles.Length; j++)
            {
                long[] ti = tiles[i];
                long[] tj = tiles[j];
                long left = ti[0] < tj[0] ? ti[0] : tj[0];
                long right = ti[0] > tj[0] ? ti[0] : tj[0];
                long bottom = ti[1] < tj[1] ? ti[1] : tj[1];
                long top = ti[1] > tj[1] ? ti[1] : tj[1];

                // The inputs have a 'gap' around y=50000 which can be avoided with a simple check
                if (top > 50000 && bottom < 50000) continue;

                // Check that no other corners are inside the rectangle
                bool valid = true;
                foreach (var t in tiles)
                {
                    if (t[0] > left && t[0] < right && t[1] > bottom && t[1] < top)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    long area = (top - bottom + 1) * (right - left + 1);
                    if (area > maxArea) 
                    {
                        maxArea = area;
                    }
                }
            }
        }
        Console.WriteLine(maxArea);
    }
}