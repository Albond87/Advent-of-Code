public class Puzzle14 : Puzzle
{
    List<int[]> robots;
    readonly int width, height, centrex, centrey;

    public Puzzle14() : base("14") {
        robots = inputs.Select(i => i.Split(["p=",","," v="],StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToList();
        width = 101;
        height = 103;
        centrex = 50;
        centrey = 51;
    }

    public override void Part1()
    {
        int seconds = 100;
        int[] counts = [0,0,0,0];
        foreach (var robot in robots) {
            int finalx = (((robot[0] + robot[2]*seconds) % width) + width) % width;
            int finaly = (((robot[1] + robot[3]*seconds) % height) + height) % height;
            if (finalx < centrex && finaly < centrey) {
                counts[0]++;
            }
            else if (finalx > centrex && finaly < centrey) {
                counts[1]++;
            }
            else if (finalx < centrex && finaly > centrey) {
                counts[2]++;
            }
            else if (finalx > centrex && finaly > centrey) {
                counts[3]++;
            }
        }
        Console.WriteLine(counts[0] * counts[1] * counts[2] * counts[3]);
    }

    public override void Part2()
    {
        // My answer is 6644 - change t_start and t_end to start and stop at different time steps
        int t_start = 6644;
        int t_end = 6644;
        int wait_ms = 300; // Wait between each time step
        foreach (var robot in robots) {
            int finalx = (((robot[0] + robot[2]*(t_start-1)) % width) + width) % width;
            int finaly = (((robot[1] + robot[3]*(t_start-1)) % height) + height) % height;
            robot[0] = finalx;
            robot[1] = finaly;
        }
        for (int t=t_start; t<=t_end; t++) {
            HashSet<Tuple<int,int>> positions = [];
            foreach (var robot in robots) {
                int newx = (((robot[0] + robot[2]) % width) + width) % width;
                int newy = (((robot[1] + robot[3]) % height) + height) % height;
                positions.Add(new(newx, newy));
                robot[0] = newx;
                robot[1] = newy;
            }
            for (int y=0; y<height; y++) {
                string line = "";
                for (int x=0; x<width; x++) {
                    if (positions.Contains(new(x,y))) {
                        line += "█";
                    }
                    else {
                        line += " ";
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------  t=" + t);
            Thread.Sleep(wait_ms);
        }
        Console.WriteLine();
    }
}