public static class Puzzle03
{
    public static int Part1(string[] lines)
    {
        return lines.Select(l => (Convert.ToInt32((byte)l.Take(l.Length/2).Intersect(l.Skip(l.Length/2)).Single()))).Select(i=>i>90?i-96:i-38).Sum();
    }

    public static int Part2(string[] lines)
    {
        int sum = 0;
        for (int i=0; i<lines.Length; i+=3)
        {
            int ascii = Convert.ToInt32((byte)lines[i].Intersect(lines[i+1]).Intersect(lines[i+2]).Single());
            sum += ascii>90 ? ascii-96 : ascii-38;
        }
        return sum;
    }
}