public static class Puzzle06
{
    public static int Part1(string input)
    {
        for (int i=3; i<input.Length; i++) {
            if (input.Substring(i-3,4).Distinct().Count() == 4) {
                return i+1;
            }
        }
        return -1;
    }

    public static int Part2(string lines)
    {
        for (int i=13; i<lines.Length; i++) {
            if (lines.Substring(i-13,14).Distinct().Count() == 14) {
                return i+1;
            }
        }
        return -1;
    }
}