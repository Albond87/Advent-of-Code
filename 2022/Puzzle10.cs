public static class Puzzle10
{
    public static int Part1(string[] lines)
    {
        int x=1;
        int ticks=0;
        int multiply=20;
        int sum=0;
        foreach (string instr in lines) {
            if (instr=="noop") {
                ticks++;
                if (ticks==multiply) {
                    sum += multiply*x;
                    multiply += 40;
                }
            } else {
                ticks += 2;
                if (ticks>=multiply) {
                    sum += multiply*x;
                    multiply += 40;
                }
                x += int.Parse(instr.Split(" ")[1]);
            }
        }
        return sum;
    }    

    public static void Part2(string[] lines)
    {
        int x=1, crtX=0, crtY=0;
        bool[][] screen = new bool[6][];
        for (int y=0; y<6; y++) screen[y] = new bool[40];
        foreach (string instr in lines) {
            DrawCRT(screen, ref crtX, ref crtY, x);
            if (instr!="noop") {
                DrawCRT(screen, ref crtX, ref crtY, x);
                x += int.Parse(instr.Split(" ")[1]);
            }
        }
        foreach (bool[] row in screen) {
            foreach (bool pixel in row) {
                Console.Write(pixel?"█":".");
            }
            Console.WriteLine();
        }
    }

    static void DrawCRT(bool[][] screen, ref int crtX, ref int crtY, int x) {
        if (x-1<=crtX && x+1>=crtX) {
            screen[crtY][crtX] = true;
        } else screen[crtY][crtX] = false;
        crtX++;
        if (crtX == 40) {
            crtX = 0;
            crtY++;
        }
    }
}