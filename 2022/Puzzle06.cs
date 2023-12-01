public static class Puzzle06
{
    public static int Part1(string lines)
    {
        for (int i=3; i<lines.Length; i++) {
            if (lines.Skip(i-3).Take(4).Distinct().Count() == 4) {
                return i+1;
            }
        }
        return -1;
    }

    public static int Part2(string lines)
    {
        for (int i=13; i<lines.Length; i++) {
            if (lines.Skip(i-13).Take(14).Distinct().Count() == 14) {
                return i+1;
            }
        }
        return -1;
    }
}