public class Puzzle06 : Puzzle
{
    public Puzzle06() : base("06") { }

    public override void Part1()
    {
        int[] times = inputs[0].Split(" ").Where(i => i != " " && i != "").Skip(1).Select(i => int.Parse(i)).ToArray();
        int[] distances = inputs[1].Split(" ").Where(i => i != " " && i != "").Skip(1).Select(i => int.Parse(i)).ToArray();
        int product = 1;
        for (int i = 0; i<times.Length; i++) {
            int b = times[i];
            int c = distances[i];
            double sqrt = Math.Sqrt(b*b - (4*c));
            double lower = (-b + sqrt) / -2;
            if ((int)lower == lower) {
                lower += 1;
            }
            double upper = (-b - sqrt) / -2;
            if ((int)upper == upper) {
                upper -= 1;
            }
            product *= (int)Math.Floor(upper)-(int)Math.Ceiling(lower)+1;
        }
        Console.WriteLine(product);
    }

    public override void Part2()
    {
        long b = long.Parse(inputs[0].Replace(" ","")[5..]);
        long c = long.Parse(inputs[1].Replace(" ","")[9..]);
        double sqrt = Math.Sqrt(b*b - (4*c));
        double lower = (-b + sqrt) / -2;
        if ((int)lower == lower) {
            lower += 1;
        }
        double upper = (-b - sqrt) / -2;
        if ((int)upper == upper) {
            upper -= 1;
        }
        Console.WriteLine((int)Math.Floor(upper)-(int)Math.Ceiling(lower)+1);
    }
}