public static class Puzzle25
{
    public static string Part1(string[] lines)
    {
        char[][] numbers = lines.ToList().Select(l=>l.Reverse().ToArray()).ToArray();
        long sum=0;
        long column;
        foreach (char[] n in numbers) {
            column=1;
            foreach (char m in n) {
                switch (m) {
                    case '2':
                        sum+=column*2;
                        break;
                    case '1':
                        sum+=column;
                        break;
                    case '-':
                        sum-=column;
                        break;
                    case '=':
                        sum-=2*column;
                        break;
                }
                column*=5;
            }
        }
        string snafu="";
        long converted=0;
        column=1;
        while ((column*2)+(column/2) < sum) column*=5;
        while (column>0) {
            if (sum-converted > column*1.5) {
                snafu+='2';
                converted+=2*column;
            }
            else if (sum-converted > column*0.5) {
                snafu+='1';
                converted+=column;
            }
            else if (sum-converted < column*-1.5) {
                snafu+='=';
                converted-=2*column;
            }
            else if (sum-converted < column*-0.5) {
                snafu+='-';
                converted-=column;
            }
            else snafu+='0';
            column/=5;
        }
        return snafu;
    }

    public static void Part2()
    {
        Console.WriteLine("Done :)");
    }
}