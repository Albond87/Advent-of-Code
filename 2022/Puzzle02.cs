public static class Puzzle02
{
    public static int Part1(string[] lines)
    {
        string[][] rounds = lines.Select(l => l.Split(" ")).ToArray();
        int score = 0;
        foreach (string[] round in rounds)
        {
            switch (round[1]) {
                case "X":
                    score += 1;
                    switch (round[0]) {
                        case "A":
                            score += 3;
                            break;
                        case "C":
                            score += 6;
                            break;
                    }
                    break;
                
                case "Y":
                    score += 2;
                    switch (round[0]) {
                        case "A":
                            score += 6;
                            break;
                        case "B":
                            score += 3;
                            break;
                    }
                    break;
                
                case "Z":
                    score += 3;
                    switch (round[0]) {
                        case "B":
                            score += 6;
                            break;
                        case "C":
                            score += 3;
                            break;
                    }
                    break;                
            }
        }
        return score;
    }

    public static int Part2(string[] lines)
    {
        string[][] rounds = lines.Select(l => l.Split(" ")).ToArray();
        int score = 0;
        foreach (string[] round in rounds)
        {
            switch (round[1]) {
                case "X":
                    switch (round[0]) {
                        case "A":
                            score += 3;
                            break;
                        case "B":
                            score += 1;
                            break;
                        case "C":
                            score += 2;
                            break;
                    }
                    break;
                
                case "Y":
                    score += 3;
                    switch (round[0]) {
                        case "A":
                            score += 1;
                            break;
                        case "B":
                            score += 2;
                            break;
                        case "C":
                            score += 3;
                            break;
                    }
                    break;
                
                case "Z":
                    score += 6;
                    switch (round[0]) {
                        case "A":
                            score += 2;
                            break;
                        case "B":
                            score += 3;
                            break;
                        case "C":
                            score += 1;
                            break;
                    }
                    break;                
            }
        }
        return score;
    }
}